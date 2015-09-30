namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class GetScreeningRequest : IntegerRequestBase
    {
        public GetScreeningRequest(int screeningId) : base(screeningId)
        {
        }
    }
}