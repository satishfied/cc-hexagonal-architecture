using System.Collections.Generic;
using System.Transactions;
using DDDSkeleton.Infrascructure.Common.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;

namespace DDDSkeleton.Repository.Memory
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _deletedAggregates;
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _insertedAggregates;
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _updatedAggregates;

        public InMemoryUnitOfWork()
        {
            _insertedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _updatedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _deletedAggregates = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
        }

        public void RegisterUpdate(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_updatedAggregates.ContainsKey(aggregateRoot))
            {
                _updatedAggregates.Add(aggregateRoot, repository);
            }
        }

        public void RegisterInsertion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_insertedAggregates.ContainsKey(aggregateRoot))
            {
                _insertedAggregates.Add(aggregateRoot, repository);
            }
        }

        public void RegisterDeletion(IAggregateRoot aggregateRoot, IUnitOfWorkRepository repository)
        {
            if (!_deletedAggregates.ContainsKey(aggregateRoot))
            {
                _deletedAggregates.Add(aggregateRoot, repository);
            }
        }

        public void Commit()
        {
            using (var scope = new TransactionScope())
            {
                foreach (var aggregateRoot in _insertedAggregates.Keys)
                {
                    var persistor = _insertedAggregates[aggregateRoot];
                    persistor.PersistInsertion(aggregateRoot);
                }

                foreach (var aggregateRoot in _updatedAggregates.Keys)
                {
                    _updatedAggregates[aggregateRoot].PersistUpdate(aggregateRoot);
                }

                foreach (var aggregateRoot in _deletedAggregates.Keys)
                {
                    _deletedAggregates[aggregateRoot].PersistDeletion(aggregateRoot);
                }

                scope.Complete();
            }
        }
    }
}