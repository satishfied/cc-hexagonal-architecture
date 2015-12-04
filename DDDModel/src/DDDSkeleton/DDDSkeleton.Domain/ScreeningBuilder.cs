using System;

namespace DDDSkeleton.Domain
{
    public class ScreeningBuilder
    {
        private Screening _screening;

        public static ScreeningBuilder CreateScreening(string candidate)
        {
            if (candidate == null)
            {
                throw new ArgumentNullException("candidate");
            }

            return new ScreeningBuilder
            {
                _screening = Screening.Create(candidate)
            };
        }

        public ScreeningBuilder ByRecruiter(string recruiter)
        {
            _screening.Recruiter = recruiter;
            return this;
        }

        public ScreeningBuilder OnDate(DateTime date)
        {
            _screening.Date = date;
            return this;
        }

        public ScreeningBuilder OnLocation(string location)
        {
            _screening.Location = location;
            return this;
        }

        public ScreeningBuilder WithGlobalEvaluation(string globalEvaluation)
        {
            _screening.GlobalEvaluation = globalEvaluation;
            return this;
        }

        public ScreeningBuilder WithRemark(string remark)
        {
            _screening.Remark = remark;
            return this;
        }

        public Screening Build()
        {
            //Validate!
            return _screening;
        }
    }
}