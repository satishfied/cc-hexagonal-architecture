using System;
using System.Collections.Generic;
using Recruiting.Domain.Infrastructure;

namespace Recruiting.Domain
{
    public class Screening:EventSourced
    {
        #region  Fields

        private string _candidate;
        private DateTime _date;

        private readonly List<ScreeningAspect> _exercises;
        private readonly List<ScreeningAspect> _knowledgeDomains;

        #endregion

        #region Constructors

        protected Screening(Guid id):base(id)
        {
            _exercises = new List<ScreeningAspect>();
            _knowledgeDomains = new List<ScreeningAspect>();
        }

        public Screening(Guid id,DateTime date, string candidate):this(id)
        {                                 
            Update(new ScreeningCreated(){Date=date,Candidate = candidate});
        }

        #endregion

        #region "Events"

        protected override void SetupEventHandlersOverride()
        {
            Handles<ScreeningCreated>(OnScreeningCreated);
            Handles<KnowledgeDomainAdded>(OnKnownledgeDomainAdded);
        }

        private void OnScreeningCreated(ScreeningCreated evt)
        {           
           _candidate = evt.Candidate;
           _date = evt.Date; 
        }

        private void OnKnownledgeDomainAdded(KnowledgeDomainAdded evt)
        {
            _knowledgeDomains.Add(evt.ScreeningAspect);
        }

        #endregion

        #region Properties
         
        public string Candidate
        {
            get
            {
                return this._candidate;
            }
        }

        public DateTime Date
        {
            get
            {
                return this._date;
            }
        }

        public IEnumerable<ScreeningAspect> Exercises
        {
            get { return this._exercises.AsReadOnly(); }
        }

        public IEnumerable<ScreeningAspect> KnowledgeDomains
        {
            get { return this._knowledgeDomains.AsReadOnly(); }
        }

        #endregion

        #region Methods

        internal class ScreeningFactory
        {
            #region Methods

            public Screening Create(DateTime date, string candidate)
            {
                return new Screening(Guid.NewGuid(),date, candidate);
            }

            #endregion
        }

        public void AddExercise(ScreeningAspect exercise)
        {
          Update(new KnowledgeDomainAdded(){ScreeningAspect = exercise});
        }

        public void AddKnowledgeDomain(ScreeningAspect knowledgeDomain)
        {
            Update(new KnowledgeDomainAdded() { ScreeningAspect = knowledgeDomain });
        }

        #endregion

    }
}