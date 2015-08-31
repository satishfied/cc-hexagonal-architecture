using System;
using Recruiting.Domain;

namespace Recruiting.ApplicationServices
{
    public class ScreeningService
    {
        private readonly IScreeningRepository repository;

        public ScreeningService(IScreeningRepository repository)
        {
            this.repository = repository;
        }

        public CreateScreeningResponse CreateScreening(CreateScreeningRequest request)
        {
            Screening screening = new Screening(Guid.NewGuid(), request.Date, request.Candidate);

            string id = this.repository.Add(screening);

            return new CreateScreeningResponse(id);
        }

        public ResultValidation<Screening> FindById(FindByIdRequest findByIdRequest)
        {
            Screening screening = this.repository.FindById(findByIdRequest.Id);
            return new ResultValidation<Screening>(screening);
        }
    }
}
