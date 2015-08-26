using System;
using System.Collections.Generic;

namespace Recruiting.Domain
{
    using Recruiting.Domain.Core;

    public class Screening:Entity
    {
        #region  Fields

        #endregion

        #region Constructors

        //EF:change:protected 
        protected Screening()
        {
            
        }

        public Screening(DateTime date, string candidate)
        {
            this.Date = date;
            this.Candidate = candidate;
            this.Exercises = new List<ScreeningAspect>();
            this.KnowledgeDomains = new List<ScreeningAspect>();
        }

        #endregion

        #region Properties

        //EF:change:virtual/protected setters needed
         
        public virtual string Candidate { get; protected set; }

        public virtual DateTime Date { get; protected set; }

        public virtual List<ScreeningAspect> Exercises { get; protected set; }

        public virtual List<ScreeningAspect> KnowledgeDomains { get; protected set; }

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
            this.Exercises.Add(exercise);
        }

        public void AddKnowledgeDomain(ScreeningAspect knowledgeDomain)
        {
            this.KnowledgeDomains.Add(knowledgeDomain);
        }

        #endregion

        //InMemory:Need
        public void SetID(Guid id)
        {
            this.ID = id;
        }
    }
}