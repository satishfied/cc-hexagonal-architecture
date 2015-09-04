using System.Collections.Generic;
using System.Linq;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ScreeningRepository : IScreeningRepository
    {
        public string Add(Screening screening)
        {
            using (var screeningContext = new ScreeningContext())
            {
                screeningContext.Screenings.Add(screening);
                screeningContext.SaveChanges();
                return screening.ID.ToString();
            }
        }

        public IEnumerable<Screening> FindAll()
        {
            using (var screeningContext = new ScreeningContext())
            {
                return screeningContext.Screenings.ToList();
            }
        }

        public Screening FindById(string id)
        {
            using (var screeningContext = new ScreeningContext())
            {
                return screeningContext.Screenings.Single(screening => screening.ID.ToString() == id);
            }
        }
    }
}
