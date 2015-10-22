using System.Collections.Generic;
using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public abstract class Evaluatable : ValueObjectBase
    {
        private readonly List<Evaluation> _evaluations = new List<Evaluation>();

        public IEnumerable<Evaluation> Evaluations
        {
            get { return _evaluations; }
        }

        public string Name { get; set; }

        public void AddEvaluation(Evaluation evaluation)
        {
            _evaluations.Add(evaluation);
        }
    }
}