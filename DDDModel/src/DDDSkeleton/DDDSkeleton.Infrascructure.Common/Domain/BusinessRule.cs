namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public class BusinessRule
    {
        private readonly string _ruleDescription;

        public BusinessRule(string ruleDescription)
        {
            _ruleDescription = ruleDescription;
        }

        public string Description
        {
            get { return _ruleDescription; }
        }
    }
}