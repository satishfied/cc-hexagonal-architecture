using System.Data.Entity;

namespace Recruiting.Data.EF
{
    public class ScreeningContext : DbContext
    {
        public DbSet<ScreeningDTO> Screenings { get; set; }

        public ScreeningContext()
            : base("ScreeningContext")
        {
        }
    }
}