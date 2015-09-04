using System;
using Recruiting.Domain.Infrastructure;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Domain
{
    public class ScreeningCreated:VersionedEvent ,IMessage
    {
        public DateTime Date { get; set; }
        public string Candidate { get; set; }
    }

    public class KnowledgeDomainAdded : VersionedEvent, IMessage
    {
        public ScreeningAspect ScreeningAspect { get; set; }
    }
}
