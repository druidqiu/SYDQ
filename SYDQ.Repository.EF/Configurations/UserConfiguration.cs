using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SYDQ.Core;

namespace SYDQ.Repository.EF.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.Id);
            Property(u => u.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Username).HasColumnName("Username").HasMaxLength(50).IsRequired();
            Property(u => u.Password).HasColumnName("Password").HasMaxLength(50);
            Property(u => u.EmailAddress).HasColumnName("EmailAddress").HasMaxLength(256);
            Property(u => u.CreatedUtc).HasColumnName("CreatedUtc");
            Property(u => u.PasswordResetToken).HasColumnName("PasswordResetToken").HasMaxLength(32);
            Property(u => u.PasswordResetTokenExpirationDate).HasColumnName("PasswordResetTokenExpirationDate");

            HasMany<Role>(r => r.Roles)
                .WithMany(r => r.Users)
                .Map(m => m.ToTable("UserRoles")
                    .MapLeftKey("UserId")
                    .MapRightKey("RoleId"));

            HasMany<EmailMessage>(u => u.Messages)
                .WithRequired(em => em.ToUser)
                .HasForeignKey(em => em.ToUserKey);
        }
    }
}
