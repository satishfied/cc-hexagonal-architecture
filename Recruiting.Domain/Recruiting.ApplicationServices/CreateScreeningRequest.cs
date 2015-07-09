namespace Recruiting.ApplicationServices
{
    using System;

    public class CreateScreeningRequest
    {
        public string Candidate
        {
            get;
            set;
        }

        public DateTime Date
        {
            get;
            set;
        }
    }
}