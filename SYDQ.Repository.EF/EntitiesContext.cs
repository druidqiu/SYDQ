using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SYDQ.Core;
using SYDQ.Repository.EF.Configurations;

namespace SYDQ.Repository.EF
{
    public class EntitiesContext : DbContext
    {
        #region Constructor

        static EntitiesContext()
        {
            DbInitializer.SetInitializer();
        }

        public EntitiesContext()
            : base("EntitiesContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }
        
        #endregion Constructor

        #region DbSet
        
        public IDbSet<User> Users { get; set; }
        
        #endregion DbSet

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
