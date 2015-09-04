 
using System;
using System.Collections.Generic;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Domain.Infrastructure
{
    /// <summary>
    /// Base class for event sourced entities.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public abstract class EventSourced:Entity,IMessagePublisher
    {
        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        private readonly List<IVersionedEvent> _pendingEvents = new List<IVersionedEvent>();
        private readonly List<IMessage> _pendingMessages = new List<IMessage>();

        private int _version = -1;

        protected EventSourced(Guid id):base(id)
        {
            SetupEventHandlers();
        }
 
        private void SetupEventHandlers()
        {
            SetupEventHandlersOverride();
        }

        protected abstract void SetupEventHandlersOverride();
 
        public int Version
        {
            get { return this._version; }
            protected set { this._version = value; }
        }

        /// <summary>
        /// Gets the collection of new events since the entity was loaded, as a consequence of command handling.
        /// </summary>
        public IEnumerable<IVersionedEvent> Events
        {
            get { return this._pendingEvents; }
        }

        public IEnumerable<IMessage> Messages
        {
            get { return this._pendingMessages; }
        }

        protected void Publish(IMessage message)
        {
            _pendingMessages.Add(message);
        }

        /// <summary>
        /// Configures a handler for an event. 
        /// </summary>
        protected void Handles<TEvent>(Action<TEvent> handler)
            where TEvent : IEvent
        {
            this._handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void LoadFrom(IEnumerable<IVersionedEvent> pastEvents)
        {
            foreach (var e in pastEvents)
            {
                this._handlers[e.GetType()].Invoke(e);
                this._version = e.Version;
            }
        }

        protected void Update(VersionedEvent e)
        {
            e.SourceId = this.Id;
            e.Version = this._version + 1;
            this._handlers[e.GetType()].Invoke(e);
            this._version = e.Version;
            this._pendingEvents.Add(e);

            var msg = e as IMessage;
            if (msg != null)
            {
                _pendingMessages.Add(msg);
            }

        }
    }
}
 