namespace DDDSkeleton.Infrascructure.Common.Domain
{
    public interface IReadOnlyRepository<TAggregate, TId> where TAggregate : IAggregateRoot
    {
        TAggregate FindBy(TId id);
    }
}