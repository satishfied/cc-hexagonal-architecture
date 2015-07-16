namespace Recruiting.Domain
{
    using System.Collections.Generic;

    public interface IScreeningRepository
    {
        #region Methods
        string Add(Screening screening);

        IEnumerable<Screening> FindAll();
        #endregion

        Screening FindById(string id);
    }
}