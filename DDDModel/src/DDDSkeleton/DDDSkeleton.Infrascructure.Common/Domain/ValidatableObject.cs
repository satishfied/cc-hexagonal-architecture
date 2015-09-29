using System.Collections.Generic;

namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public abstract class ValidatableObject
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();

            Validate();

            return _brokenRules;
        }

        protected abstract void Validate();

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }
    }
}