using System;
using System.Collections.Generic;
using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class Screening : EntityBase<int>, IAggregateRoot
    {
        private readonly string _candidate;
        private readonly List<Excercise> _excercises = new List<Excercise>();
        private readonly List<KnowledgeDomain> _knowledgeDomains = new List<KnowledgeDomain>();

        private Screening(int id, string candidate) : base(id)
        {
            _candidate = candidate;
        }

        public string Candidate
        {
            get { return _candidate; }
        }

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

        public static Screening Create(string candidate)
        {
            if (string.IsNullOrEmpty(candidate))
            {
                throw new ArgumentNullException("candidate");
            }

            return new Screening(0, candidate);
        }

        public static Screening Load(int id, string candidate)
        {
            return new Screening(id, candidate);
        }

        public void AddKnowLedgeDomain(KnowledgeDomain knowledgeDomain)
        {
            _knowledgeDomains.Add(knowledgeDomain);
        }

        public void AddExcercise(Excercise excercise)
        {
            _excercises.Add(excercise);
        }

        public void ClearExercises()
        {
            _excercises.Clear();
        }

        public void ClearKnowledgeDomains()
        {
            _knowledgeDomains.Clear();
        }
    }
}