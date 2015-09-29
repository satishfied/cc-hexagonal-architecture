namespace DDDSkeleton.Infrastructure.Common.Domain
{
    public class BusinessRule
    {
        private readonly string _ruleDescription;

        public BusinessRule(string ruleDescription)
        {
            _ruleDescription = ruleDescription;
        }

        public string RuleDescription => _ruleDescription;
    }
}