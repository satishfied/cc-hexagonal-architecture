using System;
using System.Linq;
using System.Text;
using DDDSkeleton.ApplicationServices.Screenings;
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
                    Screening = screening
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

        public InsertScreeningResponse InsertScreening(InsertScreeningRequest request)
        {
            ThrowExceptionWhenScreeningInvalid(request.Screening);

            try
            {
                _screeningRepository.Insert(request.Screening);
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
                
                ThrowExceptionWhenScreeningInvalid(request.Screening);

                _screeningRepository.Update(request.Screening);
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

        private void ThrowExceptionWhenScreeningInvalid(Screening screening)
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