using DDDSkeleton.ApplicationServices.ViewModels;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class InsertScreeningRequest : ServiceRequestBase
    {
        public ScreeningProperties Screening { get; set; }
    }
}