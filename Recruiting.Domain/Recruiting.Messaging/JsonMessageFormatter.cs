﻿using System;
using System.IO;
using System.Messaging;
using System.Text;
using Newtonsoft.Json;

namespace Recruiting.Messaging
{
    public class JsonMessageFormatter : IMessageFormatter
    {
        private static readonly JsonSerializerSettings DefaultSerializerSettings =
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            };

        private readonly JsonSerializerSettings _serializerSettings;


        public Encoding Encoding { get; set; }


        public JsonMessageFormatter(Encoding encoding = null)
            : this(encoding, null)
        {
        }

        internal JsonMessageFormatter(Encoding encoding, JsonSerializerSettings serializerSettings = null)
        {
            Encoding = encoding ?? Encoding.UTF8;
            _serializerSettings = serializerSettings ?? DefaultSerializerSettings;
        }


        public bool CanRead(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            var stream = message.BodyStream;

            return stream != null
                   && stream.CanRead
                   && stream.Length > 0;
        }

        public object Clone()
        {
            return new JsonMessageFormatter(Encoding, _serializerSettings);
        }

        public object Read(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (CanRead(message) == false)
                return null;

            using (var reader = new StreamReader(message.BodyStream, Encoding))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject(json, _serializerSettings);
            }
        }

        public void Write(Message message, object obj)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (obj == null)
                throw new ArgumentNullException("obj");

            string json = JsonConvert.SerializeObject(value: obj, formatting: Newtonsoft.Json.Formatting.None, settings: this._serializerSettings);

            message.BodyStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(json));

            //Need to reset the body type, in case the same message
            //is reused by some other formatter.
            message.BodyType = 0;
        }
    }
}