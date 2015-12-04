using System;
using System.Linq;
using System.Text;
using DDDSkeleton.ApplicationServices.ModelConversions;
using DDDSkeleton.ApplicationServices.Screenings;
using DDDSkeleton.ApplicationServices.ViewModels;
using DDDSkeleton.Domain;
using DDDSkeleton.Infrascructure.Common.UnitOfWork;

namespace DDDSkeleton.ApplicationServices.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ScreeningService(IScreeningRepository screeningRepository, IUnitOfWork unitOfWork)
        {
            _screeningRepository = screeningRepository;
            _unitOfWork = unitOfWork;
        }

        public GetScreeningResponse GetScreening(GetScreeningRequest request)
        {
            try
            {
                var screening = _screeningRepository.FindBy(request.Id);
                if (screening == null)
                {
                    return new GetScreeningResponse
                    {
                        Exception = GetStandardScreeningNotFoudException()
                    };
                }

                return new GetScreeningResponse
                {
                    ScreeningViewModel = screening.ConvertToViewModel()
                };
            }
            catch (Exception ex)
            {
                return new GetScreeningResponse
                {
                    Exception = ex
                };
            }
        }

        public GetScreeningsResponse GetAllScreenings()
        {
            try
            {
                var allScreenings = _screeningRepository.FindAll();
                return new GetScreeningsResponse {Screenings = allScreenings.ConvertToViewModels()};
            }
            catch (Exception ex)
            {
                return new GetScreeningsResponse {Exception = ex};
            }
        }

        public InsertScreeningResponse InsertScreening(InsertScreeningRequest request)
        {
            var input = request.Screening;

            var screening = ScreeningBuilder.CreateScreening(input.Candidate)
                .ByRecruiter(input.Recruiter)
                .OnLocation(input.Location)
                .OnDate(input.Date)
                .WithGlobalEvaluation(input.GlobalEvaluation)
                .WithRemark(input.Remark)
                .Build();

            foreach (var knowledgeDomainProperties in request.Screening.KnowledgeDomainProperties)
            {
                var knowledgeDomain = KnowledgeDomainBuilder.Create(knowledgeDomainProperties.Name)
                    .Build();

                if (knowledgeDomainProperties.EvaluationProperties != null)
                {
                    foreach (var evaluationProperties in knowledgeDomainProperties.EvaluationProperties)
                    {
                        var evaluation = EvaluationBuilder.Create()
                            .WithRemark(evaluationProperties.Remark)
                            .WithScores((Evaluation.EvaluationScores) evaluationProperties.Score)
                            .Build();

                        knowledgeDomain.AddEvaluation(evaluation);
                    }
                }

                screening.AddKnowLedgeDomain(knowledgeDomain);
            }

            foreach (var excerciseProperties in request.Screening.ExcerciseProperties)
            {
                var excercise = ExcerciseBuilder.Create(excerciseProperties.Name)
                    .Build();

                if (excerciseProperties.EvaluationProperties != null)
                {
                    foreach (var evaluationProperties in excerciseProperties.EvaluationProperties)
                    {
                        var evaluation = EvaluationBuilder.Create()
                            .WithRemark(evaluationProperties.Remark)
                            .WithScores((Evaluation.EvaluationScores) evaluationProperties.Score)
                            .Build();

                        excercise.AddEvaluation(evaluation);
                    }

                    screening.AddExcercise(excercise);
                }
            }

            try
            {
                _screeningRepository.Insert(screening);
                _unitOfWork.Commit();

                return new InsertScreeningResponse();
            }
            catch (Exception ex)
            {
                return new InsertScreeningResponse {Exception = ex};
            }
        }

        public UpdateScreeningResponse UpdateScreening(UpdateScreeningRequest request)
        {
            try
            {
                var screening = _screeningRepository.FindBy(request.Id);
                if (screening == null)
                {
                    return new UpdateScreeningResponse {Exception = GetStandardScreeningNotFoudException()};
                }

                // bestaande updaten
                AssignAvailablepropertiesToDomain(screening, request.Screening);
                ThrowExceptionWhenScreeningInvalid(screening);

                _screeningRepository.Update(screening);
                _unitOfWork.Commit();

                return new UpdateScreeningResponse();
            }
            catch (Exception ex)
            {
                return new UpdateScreeningResponse {Exception = ex};
            }
        }

        public DeleteScreeningResponse DeleteScreening(DeleteScreeningRequest request)
        {
            try
            {
                var screening = _screeningRepository.FindBy(request.Id);
                if (screening == null)
                {
                    return new DeleteScreeningResponse
                    {
                        Exception = GetStandardScreeningNotFoudException()
                    };
                }

                _screeningRepository.Delete(screening);
                _unitOfWork.Commit();

                return new DeleteScreeningResponse();
            }
            catch (Exception ex)
            {
                return new DeleteScreeningResponse
                {
                    Exception = ex
                };
            }
        }

        private void AssignAvailablepropertiesToDomain(Screening screening, ScreeningProperties screeningProperties)
        {
            screening.Recruiter = screeningProperties.Recruiter;
            screening.Date = screeningProperties.Date;
            screening.Location = screeningProperties.Location;
            screening.Remark = screeningProperties.Remark;
            screening.GlobalEvaluation = screeningProperties.GlobalEvaluation;

            foreach (var excerciseProperty in screeningProperties.ExcerciseProperties)
            {
                var excercice = ExcerciseBuilder.Create(excerciseProperty.Name)
                    .Build();

                if (excerciseProperty.EvaluationProperties != null)
                {
                    foreach (var evaluationProperty in excerciseProperty.EvaluationProperties)
                    {
                        var evaluation = EvaluationBuilder.Create()
                            .WithRemark(evaluationProperty.Remark)
                            .WithScores((Evaluation.EvaluationScores) evaluationProperty.Score)
                            .Build();

                        excercice.AddEvaluation(evaluation);
                    }
                }
                screening.AddExcercise(excercice);
            }

            foreach (var knowledgeDomainProperty in screeningProperties.KnowledgeDomainProperties)
            {
                var knowledgeDomain = KnowledgeDomainBuilder.Create(knowledgeDomainProperty.Name)
                    .Build();

                if (knowledgeDomainProperty.EvaluationProperties != null)
                {
                    foreach (var evaluationProperty in knowledgeDomainProperty.EvaluationProperties)
                    {
                        var evaluation = EvaluationBuilder.Create()
                            .WithRemark(evaluationProperty.Remark)
                            .WithScores((Evaluation.EvaluationScores) evaluationProperty.Score)
                            .Build();

                        knowledgeDomain.AddEvaluation(evaluation);
                    }
                }
                screening.AddKnowLedgeDomain(knowledgeDomain);
            }
        }

        private static void ThrowExceptionWhenScreeningInvalid(Screening screening)
        {
            var brokenRules = screening.GetBrokenRules().ToArray();

            if (brokenRules.Any())
            {
                var brokenRulesBuilder = new StringBuilder();
                brokenRulesBuilder.AppendLine("There was a problem saving the screening object :");
                foreach (var businessRule in brokenRules)
                {
                    brokenRulesBuilder.AppendLine(businessRule.Description);
                }
                throw new Exception(brokenRulesBuilder.ToString());
            }
        }

        private static Exception GetStandardScreeningNotFoudException()
        {
            return new ResourceNotFoundException("The requested screening was not found!");
        }
    }
}