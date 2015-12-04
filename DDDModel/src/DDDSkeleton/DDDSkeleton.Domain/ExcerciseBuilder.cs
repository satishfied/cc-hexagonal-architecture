using System;

namespace DDDSkeleton.Domain
{
    public class ExcerciseBuilder
    {
        private Excercise _excercise;

        public static ExcerciseBuilder Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return new ExcerciseBuilder
            {
                _excercise = Excercise.Create(name)
            };
        }

        public Excercise Build()
        {
            //Validate!
            return _excercise;
        }
    }
}