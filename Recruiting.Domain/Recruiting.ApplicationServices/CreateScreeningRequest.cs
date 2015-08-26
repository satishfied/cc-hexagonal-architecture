namespace Recruiting.ApplicationServices
{
    using System;
    using System.Collections.Generic;

    public class CreateScreeningRequest
    {
        public string Candidate
        {
            get;
            set;
        }

        public DateTime? Date
        {
            get;
            set;
        }

        public List<KnownledgeDomain> KnownledgeDomains { get; set; }

    }

    public abstract class KnownledgeDomainRequestBase
    {
        public string Name { get; set; }
        public IList<Score> Scores { get; set; }
    }
      public class KnownledgeDomain
    {
        public string Name { get; set; }
        public IList<Score> Scores { get; set; }
    }
    public class CreateKnownledgeDomainRequest : KnownledgeDomainRequestBase
    {
       
    }
    public class UpdateKnownledgeDomainRequest : KnownledgeDomainRequestBase
    {
        public Guid Id { get; set; }
    }

    public class DeleteKnownledgeDomainRequest
    {
          public Guid Id { get; set; }
    }

    public class Score
    {
        public string Remarks { get; set; }
        public int Scoring { get; set; }
    }

}