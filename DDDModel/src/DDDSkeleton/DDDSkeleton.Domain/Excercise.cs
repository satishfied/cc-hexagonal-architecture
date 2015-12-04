using System;

namespace DDDSkeleton.Domain
{
    public class Excercise : Evaluatable
    {
        private Excercise(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public static Excercise Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return new Excercise(name);
        }
    }
}