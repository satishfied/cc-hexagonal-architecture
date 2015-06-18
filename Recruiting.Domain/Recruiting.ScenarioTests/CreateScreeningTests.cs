using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruiting.ApplicationServices;
using Recruiting.Domain;

namespace Recruiting.ScenarioTests
{
    [TestClass]
    public class CreateScreeningTests
    {
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
            ScreeningService screeningService = new ScreeningService();

            screeningService.CreateScreening(new DateTime(2015, 02, 24), "Luc Leysen");
        }
    }
}
