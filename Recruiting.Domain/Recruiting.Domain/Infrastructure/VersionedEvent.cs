using System;

namespace Recruiting.Domain.Infrastructure
{
    public abstract class VersionedEvent : IVersionedEvent
    {
        public Guid SourceId { get; protected internal set; }
        public int Version { get; protected internal set; }
    }
}
