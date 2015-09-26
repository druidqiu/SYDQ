using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SYDQ.Repository.EF
{
    public class ThreadStorageContainer : IEntitiesContextStorageContainer
    {
        private static readonly Hashtable _EFContexts = new Hashtable();
        public EntitiesContext GetCurrentContext()
        {
            EntitiesContext context = null;
            if (_EFContexts.Contains(GetThreadName()))
            {
                context = (EntitiesContext)_EFContexts[GetThreadName()];
            }
            return context;
        }

        public void Store(EntitiesContext dataContext)
        {
            if (_EFContexts.Contains(GetThreadName()))
            {
                _EFContexts[GetThreadName()] = dataContext;
            }
            else
            {
                _EFContexts.Add(GetThreadName(), dataContext);
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
