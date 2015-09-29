namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public class BusinessRule
    {
        private readonly string _ruleDescription;

        public BusinessRule(string ruleDescription)
        {
            _ruleDescription = ruleDescription;
        }
    }
}