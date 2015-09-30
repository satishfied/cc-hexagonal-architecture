namespace DDDSkeleton.Domain
{
    public class KnowledgeDomain : Evaluatable
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(KnowledgeDomainBusinessRule.KnowledgeDomainNameRequired);
            }
        }
    }
}