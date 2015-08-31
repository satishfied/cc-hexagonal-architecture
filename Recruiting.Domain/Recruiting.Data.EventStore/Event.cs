using System;

namespace Recruiting.Data.EventStore
{
    class Event
    {
        public Guid AggregateId { get; set; }
        public string AggregateType { get; set; }
        public int Version { get; set; }
        public string Message { get; set; }
        public string CorrelationId { get; set; }
    } 
}
