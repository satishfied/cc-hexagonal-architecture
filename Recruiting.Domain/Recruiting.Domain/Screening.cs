using System;
using System.Collections.Generic;

namespace Recruiting.Domain
{
    public class Screening
    {
        #region  Fields

        private readonly string candidate;
        private readonly DateTime date;

        private readonly List<ScreeningAspect> exercises;
        private readonly List<ScreeningAspect> knowledgeDomains;

        #endregion

        #region Constructors

        private Screening()
        {
        }

        public Screening(DateTime date, string candidate)
        {
            this.date = date;
            this.candidate = candidate;
            this.exercises = new List<ScreeningAspect>();
            this.knowledgeDomains = new List<ScreeningAspect>();
        }

        #endregion

        #region Properties

        public int ID { get; set; }

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

        public IEnumerable<ScreeningAspect> Exercises
        {
            get { return this.exercises.AsReadOnly(); }
        }

        public IEnumerable<ScreeningAspect> KnowledgeDomains
        {
            get { return this.knowledgeDomains.AsReadOnly(); }
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

        public void AddExercise(ScreeningAspect exercise)
        {
            this.exercises.Add(exercise);
        }

        public void AddKnowledgeDomain(ScreeningAspect knowledgeDomain)
        {
            this.knowledgeDomains.Add(knowledgeDomain);
        }

        #endregion

        public class Serializer
        {
            public static Screening Deserialize(dynamic data)
            {
                return new Screening(data.date, data.candidate);
            }

            public static dynamic Serialize(Screening screening)
            {
                return new { date = screening.Date, candidate = screening.Candidate };
            }
        }

    }
}