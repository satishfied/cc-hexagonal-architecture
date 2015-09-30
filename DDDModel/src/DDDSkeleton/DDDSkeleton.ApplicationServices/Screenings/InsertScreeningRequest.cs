using DDDSkeleton.Domain;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class InsertScreeningRequest : ServiceRequestBase
    {
        public Screening Screening { get; set; }
    }
}