using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using Recruiting.Data.EventStore; 
using Recruiting.Domain;
using Recruiting.Domain.Infrastructure;

namespace Recruiting.ScenarioTests
{
    [TestClass]
    public class CreateScreeningTests
    {
        private IAggregateRootRepository<Screening> _repository;

        [TestInitialize]
        public void Initialize()
        {
            this._repository = new AggregateRootRepository<Screening>();
        }

        [TestMethod]
        public void CreateScreeningTests_CreateScreening()
        {
            string unitOfWorkId = Guid.NewGuid().ToString();
            Guid id = Guid.NewGuid();
            var screening = new Screening(id, DateTime.Now, "Candi Date");

            _repository.Add(screening,unitOfWorkId);

            var readRepository = new AggregateRootRepository<Screening>();
            var resultFromDb = readRepository.Get(id);

            Assert.AreEqual(id,resultFromDb.Id);
        }
    }
}
