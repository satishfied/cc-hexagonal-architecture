namespace DDDSkeleton.Domain
{
    public class Excercise : Evaluatable
    {
        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                AddBrokenRule(ExerciseBusinessRule.ExerciseNameRequired);
            }
        }
    }
}