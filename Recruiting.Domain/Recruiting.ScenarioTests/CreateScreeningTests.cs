using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruiting.ApplicationServices;
using Recruiting.Data.InMemory;
using Recruiting.Domain;

namespace Recruiting.ScenarioTests
{
    [TestClass]
    public class CreateScreeningTests
    {
        private IScreeningRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            this.repository = new InMemoryScreeningRepository();
        }

        [TestMethod]
        public void CreateScreening_x_ScreeningPersisted()
        {
            Screening.ScreeningFactory screeningFactory = new Screening.ScreeningFactory();

            var screening = screeningFactory.Create(new DateTime(2015, 02, 24), "Luc Leysen");

            Assert.IsNotNull(screening);
        }

        [TestMethod]
        public void ScreeningService_CreateScreening_ScreeningPersisted()
        {
            const string CANDIDATE = "Luc Leysen";
            var date = new DateTime(2015, 2, 24);

            var screeningService = new ScreeningService(this.repository);
            
            var createScreeningRequest = new CreateScreeningRequest
            {
                Candidate = CANDIDATE,
                Date = date
            };
            
            var createScreeningResponse = screeningService.CreateScreening(createScreeningRequest);

            var findByIdRequest = new FindByIdRequest
            {
                Id = createScreeningResponse.Id
            };

            var findByIdResponse = screeningService.FindById(findByIdRequest);

            Assert.IsTrue(findByIdResponse.Succeeded);
            Assert.AreEqual(CANDIDATE, findByIdResponse.Result.Candidate);
            Assert.AreEqual(date, findByIdResponse.Result.Date);
        }
    }
}
