using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SYDQ.Core;

namespace SYDQ.Repository.EF.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            ToTable("Role");
            HasKey(r => r.Id);
            Property(r => r.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(r => r.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
        }
    }
}
