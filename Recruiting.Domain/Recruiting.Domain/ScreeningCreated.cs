using System;
using Recruiting.Domain.Infrastructure;

namespace Recruiting.Domain
{
    public class ScreeningCreated:VersionedEvent
    {
        public DateTime Date { get; set; }
        public string Candidate { get; set; }
    }

    public class KnowledgeDomainAdded : VersionedEvent
    {
        public ScreeningAspect ScreeningAspect { get; set; }
    }
}
