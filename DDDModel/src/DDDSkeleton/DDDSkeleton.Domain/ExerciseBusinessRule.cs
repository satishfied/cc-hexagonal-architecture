using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public static class ExerciseBusinessRule
    {
        public static readonly BusinessRule ExerciseNameRequired =
            new BusinessRule("The name of the exercice is required.");
    }
}