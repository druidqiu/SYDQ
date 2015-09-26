using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Commit();
        void Rollback();
    }
}
