namespace Recruiting.Domain
{
    using Recruiting.Domain.Core;

    public class Score:Entity
    {
        //EF:change:virtual/protected setters needed

        public virtual string Remarks { get;protected set; }
        public virtual int Scoring { get; protected set; }
    }
}
