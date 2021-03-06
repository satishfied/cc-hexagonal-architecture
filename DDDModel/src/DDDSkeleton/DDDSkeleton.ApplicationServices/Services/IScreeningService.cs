﻿using DDDSkeleton.ApplicationServices.Screenings;

namespace DDDSkeleton.ApplicationServices.Services
{
    public interface IScreeningService
    {
        GetScreeningResponse GetScreening(GetScreeningRequest request);
        GetScreeningsResponse GetAllScreenings();
        InsertScreeningResponse InsertScreening(InsertScreeningRequest request);
        UpdateScreeningResponse UpdateScreening(UpdateScreeningRequest request);
        DeleteScreeningResponse DeleteScreening(DeleteScreeningRequest request);
    }
}