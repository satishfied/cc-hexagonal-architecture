using System;

namespace Recruiting.ApplicationServices
{
    using Recruiting.Domain;

    public class ScreeningService
    {
        private readonly IScreeningRepository repository;

        public ScreeningService(IScreeningRepository repository)
        {
            this.repository = repository;
        }

        public void CreateScreening(DateTime date, string candidate)
        {
            throw new NotImplementedException();
        }
    }
}
