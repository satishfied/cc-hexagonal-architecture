using DDDSkeleton.Domain;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class UpdateScreeningRequest : IntegerRequestBase
    {
        public UpdateScreeningRequest(int screeningId) : base(screeningId)
        {
        }

        public Screening Screening { get; set; }
    }
}