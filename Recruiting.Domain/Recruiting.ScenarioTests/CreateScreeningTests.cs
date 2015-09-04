using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruiting.ApplicationServices;
using Recruiting.Data.EF;
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
            this.repository = new ScreeningRepository();
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

        [TestMethod]
        public void AddKnowledgeDomainTest_KnowledgeDomainAddedToScreening()
        {
            const string KNOWLEDGE_DOMAIN_NAME = "OO";

            Screening.ScreeningFactory screeningFactory = new Screening.ScreeningFactory();

            var screening = screeningFactory.Create(new DateTime(2015, 02, 24), "Luc Leysen");
            
            screening.AddKnowledgeDomain(new ScreeningAspect(KNOWLEDGE_DOMAIN_NAME));

            Assert.AreEqual(1, screening.KnowledgeDomains.Count());
            Assert.AreEqual(KNOWLEDGE_DOMAIN_NAME, screening.KnowledgeDomains.First().Name);
        } 
        
        [TestMethod]
        public void AddExerciseTest_ExerciseAddedToScreening()
        {
            const string EXERCISE_NAME = "OO";

            Screening.ScreeningFactory screeningFactory = new Screening.ScreeningFactory();

            var screening = screeningFactory.Create(new DateTime(2015, 02, 24), "Luc Leysen");
            
            screening.AddExercise(new ScreeningAspect(EXERCISE_NAME));

            Assert.AreEqual(1, screening.Exercises.Count());
            Assert.AreEqual(EXERCISE_NAME, screening.Exercises.First().Name);
        }

        [TestMethod]
        public void AddExerciseTest_CreateScreening_ExercisePersisted()
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
