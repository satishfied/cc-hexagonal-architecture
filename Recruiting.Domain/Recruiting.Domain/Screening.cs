namespace Recruiting.Domain
{
    using System;

    public class Screening
    {
        #region  Fields

        private readonly string candidate;
        private readonly DateTime date;

        #endregion

        #region Constructors

        public Screening(DateTime date, string candidate)
        {
            this.date = date;
            this.candidate = candidate;
        }

        #endregion

        #region Properties

        public string Candidate
        {
            get
            {
                return this.candidate;
            }
        }

        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }

        #endregion

        #region Methods

        internal class ScreeningFactory
        {
            #region Methods

            public Screening Create(DateTime date, string candidate)
            {
                return new Screening(date, candidate);
            }

            #endregion
        }

        #endregion
    }
}