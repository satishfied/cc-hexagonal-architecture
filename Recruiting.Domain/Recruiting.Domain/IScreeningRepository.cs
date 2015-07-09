namespace Recruiting.Domain
{
    using System.Collections.Generic;

    public interface IScreeningRepository
    {
        #region Methods

        string Create(Screening screening);

        IEnumerable<Screening> FindAll();
        void PersistUpdate(Screening screening);

        #endregion

        Screening FindById(string id);
    }
}