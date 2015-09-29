namespace DDDSkeleton.Infrastructure.Common.Domain
{
    public abstract class EntityBase<TId> : ValidatableObject
    {
        public TId Id { get; set; }

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

        public static bool operator !=(EntityBase<TId> firstEntity, EntityBase<TId> secondenEntity)
        {
            return !(firstEntity == secondenEntity);
        }

        public override bool Equals(object entity)
        {
            return entity is EntityBase<TId> && this == (EntityBase<TId>) entity;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}