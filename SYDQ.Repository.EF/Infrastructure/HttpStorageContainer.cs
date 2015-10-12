using System.Web;

namespace SYDQ.Repository.EF
{
    public class HttpStorageContainer : IEntitiesContextStorageContainer
    {
        private readonly string _dataContextKey = "EF_CONTEXT_STORAGE_CONTAINER";

        public EntitiesContext GetCurrentContext()
        {
            try
            {
                EntitiesContext objectContext = null;
                if (HttpContext.Current.Items.Contains(_dataContextKey))
                    objectContext = (EntitiesContext)HttpContext.Current.Items[_dataContextKey];
                return objectContext;
            }
            catch { return null; }

        }

        public void Store(EntitiesContext context)
        {
            if (HttpContext.Current.Items.Contains(_dataContextKey))
                HttpContext.Current.Items[_dataContextKey] = context;
            else
                HttpContext.Current.Items.Add(_dataContextKey, context);
        }
    }
}
