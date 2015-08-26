using Recruiting.Domain;

namespace Recruiting.ApplicationServices
{
    using System;

    public class ScreeningService
    {
        private readonly IScreeningRepository repository;

        public ScreeningService(IScreeningRepository repository)
        {
            this.repository = repository;
        }

        public CreateScreeningResponse CreateScreening(CreateScreeningRequest request)
        {

            if (!request.Date.HasValue)
                request.Date = DateTime.Now;

            Screening screening = new Screening(request.Date.Value, request.Candidate);

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
