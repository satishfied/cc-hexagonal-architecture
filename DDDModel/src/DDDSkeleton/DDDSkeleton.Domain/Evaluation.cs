using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class Evaluation : ValueObjectBase
    {
        public enum EvaluationScores
        {
            Neutral = 0,
            Bad = 1,
            Good = 2
        }

        public string Remark { get; set; }
        public EvaluationScores Score { get; set; }

        protected override void Validate()
        {
        }
    }
}