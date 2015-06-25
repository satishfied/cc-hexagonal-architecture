namespace Recruiting.ScenarioTests.InMemory
{
    using System.Collections.Generic;
    using Recruiting.Domain;
    
    public class InMemoryScreeningRepository : IScreeningRepository
    {
        #region  Fields

        private readonly List<Screening> screenings = new List<Screening>();

        #endregion

        #region Properties

        public List<Screening> Screenings
        {
            get
            {
                return this.screenings;
            }
        }

        #endregion

        #region Methods

        public IEnumerable<Screening> FindAll()
        {
            return this.Screenings;
        }

        public void PersistUpdate(Screening screening)
        {
            if (!this.Screenings.Contains(screening))
            {
                this.Screenings.Add(screening);
            }
        }

        #endregion
    }
}