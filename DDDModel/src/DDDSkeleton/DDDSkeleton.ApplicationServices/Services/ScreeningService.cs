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
            var screening = AssignAvailablepropertiesToDomain(request.Screening);
            ThrowExceptionWhenScreeningInvalid(screening);

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

                var existingScreening = AssignAvailablepropertiesToDomain(request.Screening);
                // TODO update existing from new
                ThrowExceptionWhenScreeningInvalid(existingScreening);

                _screeningRepository.Update(existingScreening);
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

        private Screening AssignAvailablepropertiesToDomain(ScreeningProperties screeningProperties)
        {
            var screening = Screening.Create(screeningProperties.Candidate);
             screening.Recruiter = screeningProperties.Recruiter;
             screening.Date = screeningProperties.Date;
             screening.Location = screeningProperties.Location;
             screening.Remark = screeningProperties.Remark;
            screening.GlobalEvaluation = screeningProperties.GlobalEvaluation;

            foreach (var excerciseProperty in screeningProperties.ExcerciseProperties)
            {
                var excercice = new Excercise {Name = excerciseProperty.Name};
                if (excerciseProperty.EvaluationProperties != null)
                {
                    foreach (var evaluationProperty in excerciseProperty.EvaluationProperties)
                    {
                        excercice.AddEvaluation(new Evaluation
                        {
                            Score = (Evaluation.EvaluationScores) evaluationProperty.Score,
                            Remark = evaluationProperty.Remark
                        });
                    }
                }
                screening.AddExcercise(excercice);
            }

            foreach (var knowledgeDomainProperty in screeningProperties.KnowledgeDomainProperties)
            {
                var knowledgeDomain = new KnowledgeDomain {Name = knowledgeDomainProperty.Name};
                if (knowledgeDomainProperty.EvaluationProperties != null)
                {
                    foreach (var evaluationProperty in knowledgeDomainProperty.EvaluationProperties)
                    {
                        knowledgeDomain.AddEvaluation(new Evaluation
                        {
                            Score = (Evaluation.EvaluationScores) evaluationProperty.Score,
                            Remark = evaluationProperty.Remark
                        });
                    }
                }
                screening.AddKnowLedgeDomain(knowledgeDomain);
            }

            return screening;
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