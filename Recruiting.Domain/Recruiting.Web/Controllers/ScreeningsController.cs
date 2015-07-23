using System.Web.Http;
using Recruiting.ApplicationServices;

namespace Recruiting.Web.Controllers
{
    public class ScreeningsController : ApiController
    {
        private readonly ScreeningService screeningService;

        public ScreeningsController(ScreeningService screeningService)
        {
            this.screeningService = screeningService;
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
