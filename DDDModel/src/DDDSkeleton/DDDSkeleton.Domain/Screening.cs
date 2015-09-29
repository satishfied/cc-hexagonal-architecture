using System;
using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class Screening : EntityBase<int>
    {
        public string Candidate { get; set; }
        public string Recruiter { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string GlobalEvaluation { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Candidate))
            {
                AddBrokenRule(ScreeningBusinessRule.CandidateRequired);
            }
        }
    }
}