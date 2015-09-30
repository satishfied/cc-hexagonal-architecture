using DDDSkeleton.Domain;

namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class GetScreeningResponse : ServiceResponseBase
    {
        public Screening Screening { get; set; }
    }
}