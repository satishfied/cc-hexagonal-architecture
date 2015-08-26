namespace Recruiting.Data.EF
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class AddIdentityDatabaseGeneratedOptionConvention:Convention
    {
        public AddIdentityDatabaseGeneratedOptionConvention()
        {
            this.Properties()
                .Where( f => f.Name == "ID")
                .Configure( p => p.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));                        
        }
    }
}