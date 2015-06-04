namespace Recruiting.Domain
{
    using System;

    internal class Screening
    {
        private readonly DateTime date;
        private readonly string candidate;

        private Screening(DateTime date, string candidate)
        {
            this.date = date;
            this.candidate = candidate;
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public string Candidate
        {
            get
            {
                return candidate;
            }
        }

        internal class ScreeningFactory
        {
            public Screening Create(DateTime date, string candidate)
            {
                return new Screening(date, candidate);
            }
        }
    }
}
