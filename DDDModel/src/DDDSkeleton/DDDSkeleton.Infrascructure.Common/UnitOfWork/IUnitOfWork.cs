using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Infrascructure.Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository);
        void Commit();
    }
}