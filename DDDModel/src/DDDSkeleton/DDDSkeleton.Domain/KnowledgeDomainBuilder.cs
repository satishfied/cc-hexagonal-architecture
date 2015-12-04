using System;

namespace DDDSkeleton.Domain
{
    public class KnowledgeDomainBuilder
    {
        private KnowledgeDomain _knowledgeDomain;

        public static KnowledgeDomainBuilder Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            return new KnowledgeDomainBuilder
            {
                _knowledgeDomain = KnowledgeDomain.Create(name)
            };
        }

        public KnowledgeDomain Build()
        {
            //Validate!
            return _knowledgeDomain;
        }
    }
}