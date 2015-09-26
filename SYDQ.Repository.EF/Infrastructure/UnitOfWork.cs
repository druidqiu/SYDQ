using SYDQ.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntitiesContext _entities;

        public UnitOfWork()
        {
            _entities = EntitiesContextFactory.GetEntitiesContext();
        }

        public bool Commit()
        {
            try
            {
                bool flag = _entities.SaveChanges() > 0;
                if (_entities.Configuration.AutoDetectChangesEnabled == false)
                    _entities.Configuration.AutoDetectChangesEnabled = true;
                return flag;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Rollback()
        {
            EntitiesContextFactory.ResetEntitiesContent();
        }
    }
}
