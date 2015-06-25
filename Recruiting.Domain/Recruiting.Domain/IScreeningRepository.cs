namespace Recruiting.Domain
{
    using System.Collections.Generic;

    public interface IScreeningRepository
    {
        #region Methods

        IEnumerable<Screening> FindAll();
        void PersistUpdate(Screening screening);

        #endregion
    }
}