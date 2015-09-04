using System;

namespace Recruiting.Domain.Infrastructure
{
    public abstract class VersionedEvent : IVersionedEvent
    {
        public Guid SourceId { get; set; }
        public int Version { get; set; }
    }
}
