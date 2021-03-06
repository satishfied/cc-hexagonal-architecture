﻿using System.Net.Http;
using System.Web.Http;
using DDDSkeleton.ApplicationServices;
using DDDSkeleton.ApplicationServices.Screenings;
using DDDSkeleton.ApplicationServices.Services;
using DDDSkeleton.ApplicationServices.ViewModels;
using DDDSkeleton.Domain;
using DDDSkeleton.WebService.Helpers;

namespace DDDSkeleton.WebService.Controllers
{
    public class ScreeningsController : ApiController
    {
        private readonly IScreeningService _screeningService;

        public ScreeningsController(IScreeningService screeningService)
        {
            _screeningService = screeningService;
        }

        public HttpResponseMessage Get()
        {
            ServiceResponseBase resp = _screeningService.GetAllScreenings();
            return Request.BuildResponse(resp);
        }

        public HttpResponseMessage Get(int id)
        {
            ServiceResponseBase resp = _screeningService.GetScreening(new GetScreeningRequest(id));
            return Request.BuildResponse(resp);
        }

        public HttpResponseMessage Post(ScreeningProperties screening)
        {
            var insertScreeningResponse =
                _screeningService.InsertScreening(new InsertScreeningRequest {Screening = screening});
            return Request.BuildResponse(insertScreeningResponse);
        }
    }
}