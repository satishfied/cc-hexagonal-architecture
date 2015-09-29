using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class KnowledgeDomain : ValueObjectBase
    {
        public string Name { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(KnowledgeDomainBusinessRule.KnowledgeDomainNameRequired);
            }
        }
    }
}