namespace Recruiting.ApplicationServices
{
    public class CreateScreeningResponse
    {
        private readonly string id;

        public CreateScreeningResponse(string id)
        {
            this.id = id;
        }

        public string Id
        {
            get
            {
                return this.id;
            }
        }
    }
}
