using System;

namespace DDDSkeleton.Domain
{
    public class KnowledgeDomain : Evaluatable
    {
        private KnowledgeDomain(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public static KnowledgeDomain Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            return new KnowledgeDomain(name);
        }
    }
}