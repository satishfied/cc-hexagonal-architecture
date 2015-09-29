using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public class Excercise : ValueObjectBase
    {
        public string Name { get; set; }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(ExerciseBusinessRule.ExerciseNameRequired);
            }
        }
    }
}