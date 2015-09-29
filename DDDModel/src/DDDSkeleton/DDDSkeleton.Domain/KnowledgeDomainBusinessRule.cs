using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public static class KnowledgeDomainBusinessRule
    {
        public static readonly BusinessRule KnowledgeDomainNameRequired =
            new BusinessRule("The name of the knowledgedomain is required.");
    }
}