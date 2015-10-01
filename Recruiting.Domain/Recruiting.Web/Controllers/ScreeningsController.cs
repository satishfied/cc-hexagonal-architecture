using System.Web.Http;
using Recruiting.ApplicationServices;
using Recruiting.Domain;

namespace Recruiting.Web.Controllers
{
    public class ScreeningsController : ApiController
    {
        private readonly ScreeningService screeningService;
        private readonly IScreeningRepository screeningRepository;

        public ScreeningsController(ScreeningService screeningService, IScreeningRepository screeningRepository)
        {
            this.screeningService = screeningService;
            this.screeningRepository = screeningRepository;
        }

        public IHttpActionResult Get()
        {
            return this.Ok(this.screeningRepository.FindAll());
        }

        public IHttpActionResult Get(string id)
        {
            var resultValidation = this.screeningService.FindById(new FindByIdRequest { Id = id });

            if (resultValidation.Succeeded)
            {
                return this.Ok(resultValidation.Result);
            }

            return this.NotFound();
        }

        public IHttpActionResult Post(CreateScreeningRequest createScreeningRequest)
        {
            return this.Ok(this.screeningService.CreateScreening(createScreeningRequest));
        }
    }
}
