using SYDQ.Core;
using SYDQ.Repository.EF.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class EntitiesContext : DbContext
    {
        #region Constructor

        static EntitiesContext()
        {
            DBInitializer.SetInitializer();
        }

        public EntitiesContext()
            : base("EntitiesContext")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
        
        #endregion Constructor

        #region DbSet
        
        public IDbSet<User> Users { get; set; }
        
        #endregion DbSet

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public void SetCommandTimeout(int? seconds)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = seconds;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new EmailMessageConfiguration());
        }
    }
}
