using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recruiting.Domain;
using Recruiting.Domain.Infrastructure.Messaging;

namespace Recruiting.Messaging.Tests
{

    using System.Reactive;
    using System.Reactive.Linq;

    [TestClass]
    public class RxMsMqMessageStreamServiceTests
    {
        [TestMethod]
        public void RxMsMqMessageStreamService_MessageStreamWriter_Dispatched_ReceivesMessagesOnStream()
        {

            //create and connect to service
            IMessageStreamService service = new MsMqMessageStreamService();

            //Testing purp
            //MessageQueue.Delete(@".\Private$\screenings");

            var streamWriter = service.OpenWriter(@"FormatName:MULTICAST=234.1.1.1:8001");
            var streamReader = service.OpenReader(@".\Private$\screenings");

            var streamReader2 = service.OpenReader(@".\Private$\screenings2");

            var screeningsCreatedForCandiDate = new List<ScreeningCreated>();
            var screeningsCreatedForOthers = new List<ScreeningCreated>();

            //use Rx ..
            streamReader.Where(x => x is ScreeningCreated)
                .Cast<ScreeningCreated>()        
                .Where(x => x.Candidate == "Candi Date")
                .Subscribe(screeningsCreatedForCandiDate.Add);

            streamReader.Where(x => x is ScreeningCreated)
               .Cast<ScreeningCreated>()
               .Where(x => x.Candidate != "Candi Date")
               .Subscribe(screeningsCreatedForOthers.Add);

            streamReader.Open();

            int streamReader2Count = 0;
            streamReader2.Where(x => x is ScreeningCreated)
                .Cast<ScreeningCreated>()
                .Subscribe((x) => streamReader2Count++);
            
            //demo:
            //do not open streamReader2
            //run test twice
            //check count on second run
            streamReader2.Open();

            screeningsCreatedForCandiDate.Clear();
            screeningsCreatedForOthers.Clear();

            //write to stream...
            var screenings = new List<Screening>();

            screenings.Add(new Screening(Guid.NewGuid(), DateTime.Now, "Candi Date"));
            screenings.Add(new Screening(Guid.NewGuid(), DateTime.Now, "No Candi Date"));
            screenings.Add(new Screening(Guid.NewGuid(), DateTime.Now, "Candi Date"));

            streamWriter.DispatchAsync(screenings.SelectMany(x => x.Messages));

            System.Threading.Thread.Sleep(5000);

            Assert.AreEqual(2, screeningsCreatedForCandiDate.Count);
            Assert.AreEqual(1, screeningsCreatedForOthers.Count);

            //Multiple consumers ..
            Assert.IsTrue( streamReader2Count > 2);
        }         
    }
}
