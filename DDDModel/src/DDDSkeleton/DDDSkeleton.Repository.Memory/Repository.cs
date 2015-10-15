using DDDSkeleton.Infrascructure.Common.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;
using DDDSkeleton.Repository.Memory.Database;

namespace DDDSkeleton.Repository.Memory
{
    public abstract class Repository<TDomain, TId, TDatabase> : IUnitOfWorkRepository where TDomain : IAggregateRoot
    {
        private readonly IObjectContextFactory _objectContextFactory;
        private readonly IUnitOfWork _unitOfWork;

        protected Repository(IUnitOfWork unitOfWork, IObjectContextFactory objectContextFactory)
        {
            _unitOfWork = unitOfWork;
            _objectContextFactory = objectContextFactory;
        }

        protected IObjectContextFactory ObjectContextFactory
        {
            get { return _objectContextFactory; }
        }

        public void PersistInsertion(IAggregateRoot aggregateRoot)
        {
            var databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().AddEntity(databaseType);
        }

        public void PersistUpdate(IAggregateRoot aggregateRoot)
        {
            var databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().UpdateEntity(databaseType);
        }

        public void PersistDeletion(IAggregateRoot aggregateRoot)
        {
            var databaseType = RetrieveDatabaseTypeFrom(aggregateRoot);
            _objectContextFactory.Create().DeleteEntity(databaseType);
        }

        public void Update(TDomain aggregate)
        {
            _unitOfWork.RegisterUpdate(aggregate, this);
        }

        public void Insert(TDomain aggregate)
        {
            _unitOfWork.RegisterInsertion(aggregate, this);
        }

        public void Delete(TDomain aggregate)
        {
            _unitOfWork.RegisterDeletion(aggregate, this);
        }

        public abstract TDatabase ConvertToDatabaseType(TDomain domainType);
        public abstract TDomain FindBy(TId id);

        private TDatabase RetrieveDatabaseTypeFrom(IAggregateRoot aggregateRoot)
        {
            var domainType = (TDomain) aggregateRoot;
            return ConvertToDatabaseType(domainType);
        }
    }
}