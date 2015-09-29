using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public static class ScreeningBusinessRule
    {
         public static readonly BusinessRule CandidateRequired = new BusinessRule("A candidate is required.");
    }
}