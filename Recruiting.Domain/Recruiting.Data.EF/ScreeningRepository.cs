using System.Collections.Generic;
using System.Linq;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ScreeningRepository : IScreeningRepository
    {
        public string Add(Screening screening)
        {
            using (ScreeningContext screeningContext = new ScreeningContext())
            {
                ScreeningDTO screeningDto = ScreeningDTO.From(screening);

                screeningContext.Screenings.Add(screeningDto);
                screeningContext.SaveChanges();

                return screeningDto.ID.ToString();
            }
        }

        public IEnumerable<Screening> FindAll()
        {
            using (ScreeningContext screeningContext = new ScreeningContext())
            {
                return screeningContext.Screenings.ToList()
                    .Select(screening => screening.ToDomain()).ToList();
            }
        }

        public Screening FindById(string id)
        {
            using (ScreeningContext screeningContext = new ScreeningContext())
            {
                return screeningContext.Screenings.Single(screening => screening.ID.ToString() == id).ToDomain();
            }
        }
    }
}
