using System;
using System.Collections;
using System.Threading;

namespace SYDQ.Repository.EF
{
    public class ThreadStorageContainer : IEntitiesContextStorageContainer
    {
        private static readonly Hashtable EfContexts = new Hashtable();
        public EntitiesContext GetCurrentContext()
        {
            EntitiesContext context = null;
            if (EfContexts.Contains(GetThreadName()))
            {
                context = (EntitiesContext)EfContexts[GetThreadName()];
            }
            return context;
        }

        public void Store(EntitiesContext dataContext)
        {
            if (EfContexts.Contains(GetThreadName()))
            {
                EfContexts[GetThreadName()] = dataContext;
            }
            else
            {
                EfContexts.Add(GetThreadName(), dataContext);
            }
        }

        private static string GetThreadName()
        {
            if (String.IsNullOrEmpty(Thread.CurrentThread.Name))
            {
                Thread.CurrentThread.Name = Guid.NewGuid().ToString();
            }
            return Thread.CurrentThread.Name;
        }
    }
}
