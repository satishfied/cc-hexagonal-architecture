using System.Collections.Generic;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ScreeningRepository : IScreeningRepository
    {
        public string Add(Screening screening)
        {
            using (ScreeningContext screeningContext = new ScreeningContext())
            {
                screeningContext.Screenings.Add(screening);
                screeningContext.SaveChanges();
                return screening.ID.ToString();
            }
        }

        public IEnumerable<Screening> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Screening FindById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
