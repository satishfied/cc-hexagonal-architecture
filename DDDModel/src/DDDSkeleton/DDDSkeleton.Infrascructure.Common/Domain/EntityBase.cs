using System.Collections.Generic;

namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public abstract class EntityBase<TId> 
    {
        protected EntityBase(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
    }
}