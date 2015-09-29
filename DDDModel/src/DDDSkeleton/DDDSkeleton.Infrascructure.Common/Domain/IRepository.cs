namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public interface IRepository<TAggregate, TId> : IReadOnlyRepository<TAggregate, TId>
        where TAggregate : IAggregateRoot
    {
        void Delete(TAggregate aggregate);
        void Insert(TAggregate aggregate);
        void Update(TAggregate aggregate);
    }
}