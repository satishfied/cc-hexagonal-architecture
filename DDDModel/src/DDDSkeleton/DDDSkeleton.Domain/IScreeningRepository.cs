using System.Collections.Generic;
using DDDSkeleton.Infrascructure.Common.Domain;

namespace DDDSkeleton.Domain
{
    public interface IScreeningRepository : IRepository<Screening, int>
    {
        IEnumerable<Screening> FindAll();
    }
}