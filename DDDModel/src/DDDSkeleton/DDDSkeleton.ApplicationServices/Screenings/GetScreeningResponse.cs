using DDDSkeleton.ApplicationServices.ViewModels;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class GetScreeningResponse : ServiceResponseBase
    {
        public ScreeningViewModel ScreeningViewModel { get; set; }
    }
}