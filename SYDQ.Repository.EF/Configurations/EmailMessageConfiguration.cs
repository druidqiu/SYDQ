using SYDQ.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF.Configurations
{
    public class EmailMessageConfiguration : EntityTypeConfiguration<EmailMessage>
    {
        public EmailMessageConfiguration()
        {
            ToTable("EmailMessage");
            HasKey(em => em.Id);
            Property(em => em.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(em => em.Body).HasColumnName("Body");
            Property(em => em.Subject).HasColumnName("Subject").HasMaxLength(100);

            this.HasOptional<User>(em => em.FromUser)
                .WithMany()
                .HasForeignKey(em => em.FromUserKey);
        }
    }
}
