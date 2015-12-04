namespace DDDSkeleton.Domain
{
    public class EvaluationBuilder
    {
        private Evaluation _evaluation;

        public static EvaluationBuilder Create()
        {
            return new EvaluationBuilder
            {
                _evaluation = new Evaluation()
            };
        }

        public EvaluationBuilder WithRemark(string remark)
        {
            _evaluation.Remark = remark;
            return this;
        }

        public EvaluationBuilder WithScores(Evaluation.EvaluationScores score)
        {
            _evaluation.Score = score;
            return this;
        }

        public Evaluation Build()
        {
            //Validate!
            return _evaluation;
        }
    }
}