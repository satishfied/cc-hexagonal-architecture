using DDDSkeleton.ApplicationServices.ViewModels;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class UpdateScreeningRequest : IntegerRequestBase
    {
        public UpdateScreeningRequest(int screeningId) : base(screeningId)
        {
        }

        public ScreeningProperties Screening { get; set; }
    }
}