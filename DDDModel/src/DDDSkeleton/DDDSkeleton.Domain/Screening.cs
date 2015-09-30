using System;
using System.Collections.Generic;
using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class Screening : EntityBase<int>
    {
        private readonly List<Excercise> _excercises = new List<Excercise>();
        private readonly List<KnowledgeDomain> _knowledgeDomains = new List<KnowledgeDomain>();
        public string Candidate { get; set; }
        public string Recruiter { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Remark { get; set; }
        public string GlobalEvaluation { get; set; }

        public IEnumerable<KnowledgeDomain> KnowledgeDomains
        {
            get { return _knowledgeDomains; }
        }

        public IEnumerable<Excercise> Excercises
        {
            get { return _excercises; }
        }

        public void AddKnowLedgeDomain(KnowledgeDomain knowledgeDomain)
        {
            _knowledgeDomains.Add(knowledgeDomain);
        }

        public void AddExcercise(Excercise excercise)
        {
            _excercises.Add(excercise);
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Candidate))
            {
                AddBrokenRule(ScreeningBusinessRule.CandidateRequired);
            }
        }
    }
}