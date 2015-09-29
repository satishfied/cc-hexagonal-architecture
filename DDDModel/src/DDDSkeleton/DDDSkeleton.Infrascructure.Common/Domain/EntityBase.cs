using System.Collections.Generic;

namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; set; }

        public override bool Equals(object entity)
        {
            return entity is EntityBase<TId> && this == (EntityBase<TId>) entity;
        }

        protected bool Equals(EntityBase<TId> other)
        {
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<TId> firstEntity, EntityBase<TId> secondEntity)
        {
            if ((object) firstEntity == null && (object) secondEntity == null)
            {
                return true;
            }

            if ((object) firstEntity == null || (object) secondEntity == null)
            {
                return false;
            }

            return firstEntity.Id.ToString() == secondEntity.Id.ToString();
        }

        public static bool operator !=(EntityBase<TId> firstEntity, EntityBase<TId> secondEntity)
        {
            return !(firstEntity == secondEntity);
        }
    }
}