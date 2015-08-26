using System.Data.Entity;
using Recruiting.Domain;

namespace Recruiting.Data.EF
{
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ScreeningContext : DbContext
    {
        public DbSet<Screening> Screenings { get; set; }

        public ScreeningContext()
            : base("ScreeningContext")
        {
             Configuration.AutoDetectChangesEnabled = true;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
            Configuration.UseDatabaseNullSemantics = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            modelBuilder.Conventions.Add(new AddIdentityDatabaseGeneratedOptionConvention());

            modelBuilder.Entity<Screening>()
                        .ToTable("Screening")
                        .HasMany(x => x.Exercises)
                        .WithRequired().Map(x=>x.MapKey("Excercise_ID"))
                        .WillCascadeOnDelete(false);
            modelBuilder.Entity<Screening>()
                        .HasMany(x => x.KnowledgeDomains)
                        .WithRequired().Map(x => x.MapKey("KnowledgeDomain_ID"))
                        .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<ScreeningAspect>()
                    .ToTable("ScreeningAspect")
                    .HasMany(x => x.Scores)
                    .WithRequired();

            //EF:because Score is child of aspect, it needs to be an entity.Complex type not allowed
            modelBuilder.Entity<Score>().ToTable("Score");
        }
    }
}