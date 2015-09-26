using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public interface IEntitiesContextStorageContainer
    {
        EntitiesContext GetCurrentContext();
        void Store(EntitiesContext dataContext);
    }
}
