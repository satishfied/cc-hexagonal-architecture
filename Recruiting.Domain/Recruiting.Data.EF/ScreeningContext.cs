using System.Data.Entity;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    public class ScreeningContext : DbContext
    {
        public DbSet<Screening> Screenings { get; set; }

        public ScreeningContext()
            : base("ScreeningContext")
        {
            
        }
    }
}