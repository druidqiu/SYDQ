using System;
using SYDQ.Infrastructure.UnitOfWork;

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
            bool flag = _entities.SaveChanges() > 0;
            if (_entities.Configuration.AutoDetectChangesEnabled == false)
                _entities.Configuration.AutoDetectChangesEnabled = true;
            return flag;
        }

        public void Rollback()
        {
            EntitiesContextFactory.ResetEntitiesContent();
        }
    }
}
