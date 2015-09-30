using System.Net.Http;
using System.Web.Http;
using DDDSkeleton.ApplicationServices;
using DDDSkeleton.ApplicationServices.Services;
using DDDSkeleton.WebService.Helpers;

namespace DDDSkeleton.WebService.Controllers
{
    public class ScreeningController : ApiController
    {
        private readonly IScreeningService _screeningService;

        public ScreeningController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        public HttpResponseMessage Get()
        {
            ServiceResponseBase resp = _screeningService.GetAllScreenings();
            return Request.BuildResponse(resp);
        }
    }
}