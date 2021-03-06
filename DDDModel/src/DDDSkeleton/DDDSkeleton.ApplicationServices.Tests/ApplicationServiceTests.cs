﻿using System;
using System.Collections.Generic;
using System.Linq;
using DDDSkeleton.ApplicationServices.Screenings;
using DDDSkeleton.ApplicationServices.Services;
using DDDSkeleton.ApplicationServices.ViewModels;
using DDDSkeleton.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;
using DDDSkeleton.Repository.Memory;
using DDDSkeleton.Repository.Memory.Database;
using DDDSkeleton.Repository.Memory.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DDDSkeleton.ApplicationServices.Tests
{
    [TestClass]
    public class ApplicationServiceTests
    {
        private IScreeningRepository _screeningRepository;
        private ScreeningService _service;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork = new InMemoryUnitOfWork();
            _screeningRepository = new ScreeningRepository(_unitOfWork, new LazySingletonObjectContextFactory());
        }

        private void CreateSut()
        {
            _service = new ScreeningService(_screeningRepository, _unitOfWork);
        }

        [TestMethod]
        public void GetScreening_ReturnsCorrectScreening()
        {
            const int screeningId = 3;

            var request = new GetScreeningRequest(screeningId);

            CreateSut();
            var result = _service.GetScreening(request);

            Assert.AreEqual(screeningId, result.ScreeningViewModel.Id);
        }

        [TestMethod]
        public void InsertScreening_InsertsCorrectScreening()
        {
            const string location = "McBoll Olen";
            const string candidate = "Gerda";
            const string recruiter = "Glen Van de Sande";
            var date = new DateTime(2015, 9, 30, 18, 30, 0);
            const string remark = "Schenkt lekkere Wieze's";
            const string globalEvaluation = "Senior biertapper";

            var screening = new ScreeningProperties
            {
                Location = location,
                Candidate = candidate,
                Recruiter = recruiter,
                Date = date,
                Remark = remark,
                GlobalEvaluation = globalEvaluation
            };

            var excercise = new ExcerciseProperties
            {
                Name = "Schenken bier"
            };
            
            const string evaluationRemark = "Schenkt grote pinten";
            var evaluation1 = new EvaluationProperties
            {
                Remark = evaluationRemark,
                Score = (int)Evaluation.EvaluationScores.Good
            };

            var evaluation2 = new EvaluationProperties
            {
                Remark = "Schenkt groede Wieze's",
                Score = (int)Evaluation.EvaluationScores.Neutral
            };

            excercise.EvaluationProperties = new List<EvaluationProperties> {evaluation1, evaluation2};
            screening.ExcerciseProperties = new List<ExcerciseProperties> { excercise };

            var knowledgeDomain = new KnowledgeDomainProperties {Name = "Schenken van een Wieze"};
            screening.KnowledgeDomainProperties = new List<KnowledgeDomainProperties> { knowledgeDomain };

            var request = new InsertScreeningRequest
            {
                Screening = screening
            };

            CreateSut();
            _service.InsertScreening(request);
            var result = _service.GetScreening(new GetScreeningRequest(4));

            Assert.IsNotNull(result);
            Assert.AreEqual(location, result.ScreeningViewModel.Location);
            Assert.AreEqual(candidate, result.ScreeningViewModel.Candidate);
            Assert.AreEqual(recruiter, result.ScreeningViewModel.Recruiter);
            Assert.AreEqual(date, result.ScreeningViewModel.Date);
            Assert.AreEqual(remark, result.ScreeningViewModel.Remark);
            Assert.AreEqual(globalEvaluation, result.ScreeningViewModel.GlobalEvaluation);

            Assert.AreEqual(1, result.ScreeningViewModel.ExcerciceViewModels.Count);
            Assert.AreEqual(2, result.ScreeningViewModel.ExcerciceViewModels.First().EvaluationViewModels.Count());

            var firstEvaluation = result.ScreeningViewModel.ExcerciceViewModels.First().EvaluationViewModels.First();
            Assert.AreEqual(evaluationRemark, firstEvaluation.Remark);
            Assert.AreEqual((int) Evaluation.EvaluationScores.Good, firstEvaluation.Score);

            Assert.AreEqual(1, result.ScreeningViewModel.KnowledgeDomainViewModels.Count());
            Assert.AreEqual(0, result.ScreeningViewModel.KnowledgeDomainViewModels.First().EvaluationViewModels.Count());
        }
    }
}