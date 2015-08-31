using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Dapper;
using Newtonsoft.Json;
using Recruiting.Domain.Infrastructure;

namespace Recruiting.Data.EventStore
{
    public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : EventSourced
    {

        private readonly string _sourceType = typeof(T).FullName + "," + typeof(T).Assembly.GetName().Name;
        private readonly JsonSerializer _serializer = new JsonSerializer();
        private readonly  DbProviderFactory _factory = DbProviderFactories.GetFactory(Properties.Settings.Default.EventSourceDbProviderName);

        public EventSourcedRepository()
        {
            _serializer.TypeNameHandling = TypeNameHandling.Objects;
            _serializer.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
        }

        private IDbConnection CreateConnection()
        {
            var connection = _factory.CreateConnection();
            connection.ConnectionString = Properties.Settings.Default.EventSourceDbConnectionString;
            return connection;
        }

        public T Find(Guid id)
        {
            using (var connection = CreateConnection())
            {                
                var events = connection.Query<Event>(
                    "select Event.* from Event where AggregateId = @AggregateId And AggregateType = @AggregateType",
                    new { AggregateId = id,
                        AggregateType = _sourceType
                    }).ToList();
                if (events.Any())
                {
                    return FromEvents(id, events);                 
                }
            }
            return null;
        }

        private T FromEvents(Guid id, IEnumerable<Event> events)
        {
            var deserialized = events.OrderBy(x => x.Version).Select(Deserialize);
            return FromEventResolver.Resolve(id, deserialized);
        }

        private static class FromEventResolver
        {
            private static ConstructorInfo ProtectedConstructor =
                       typeof(T).GetConstructor(
                           BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, null,
                           new[] { typeof(Guid) }, null);

            private static MethodInfo ProtectedLoad =
                typeof(T).GetMethod("LoadFrom",
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance, null,
                    new Type[] { typeof(IEnumerable<IVersionedEvent>) }, null);

            public static T Resolve(Guid id, IEnumerable<IVersionedEvent> events)
            {

                if (ProtectedConstructor == null)
                {
                    throw new NotImplementedException("Invalid or missing constructor. EventSourcedRepository needs constructor (Guid id).");
                }
                if (ProtectedLoad == null)
                {
                    throw new NotImplementedException("Invalid or missing LoadFrom method. EventSourcedRepository needs protected method LoadFrom(IEnumerable<VersionedEvent> events).");
                }
                var result = (T)ProtectedConstructor.Invoke(new object[] { id });
                ProtectedLoad.Invoke(result, new object[] { events });

                return result;
            }

        }

        public T Get(Guid id)
        {
            var result=  this.Find(id);
            if (result == null)
                throw new NotSupportedException("EntityNotFoundException");
            return result;
        }

        public void Add(T eventSourced, string correlationId)
        {           
            using (var connection = CreateConnection())
            {
                connection.Open();
                foreach (var @event in eventSourced.Events)
                {
                    var newEvent = this.Serialize(@event, correlationId);
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "insert into Event (AggregateId,AggregateType,Version,Message) values (@AggregateId,@AggregateType,@Version,@Message)";

                        AddParameter(command, "@AggregateId", newEvent.AggregateId);
                        AddParameter(command, "@AggregateType", newEvent.AggregateType);
                        AddParameter(command, "@Version", newEvent.Version);
                        AddParameter(command, "@Message", newEvent.Message);
                        command.ExecuteNonQuery();
                    }
                } 
            }
        }

        private static IDbCommand AddParameter(IDbCommand command, string name, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            command.Parameters.Add(p);
            return command;
        }

        private Event Serialize(IVersionedEvent e, string correlationId)
        {
            Event serialized;  
            using (var writer = new StringWriter())
            {
                this._serializer.Serialize(writer, e);
                serialized = new Event
                {
                    AggregateId = e.SourceId,
                    AggregateType = _sourceType,
                    Version = e.Version,
                    Message = writer.ToString(),
                    CorrelationId = correlationId
                };
            }
            return serialized;
        }

        private IVersionedEvent Deserialize(Event @event)
        {  
            using (var reader = new StringReader(@event.Message))
            {
                var versionedEventType = Type.GetType(_sourceType);
                var storageVersion = this._serializer.Deserialize((TextReader)reader, typeof(IVersionedEvent));
                return (IVersionedEvent) storageVersion;
            }
        }
    }
}