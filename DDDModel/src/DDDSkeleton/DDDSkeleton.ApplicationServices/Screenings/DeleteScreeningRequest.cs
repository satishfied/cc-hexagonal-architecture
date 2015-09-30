namespace DDDSkeleton.ApplicationServices.Screenings
{
    public class DeleteScreeningRequest : IntegerRequestBase
    {
        public DeleteScreeningRequest(int screeningId) : base(screeningId)
        {
        }
    }
}