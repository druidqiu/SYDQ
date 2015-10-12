namespace SYDQ.Repository.EF
{
    public class EntitiesContextFactory
    {
        private static IEntitiesContextStorageContainer _entitiesContextStorageContainer;

        public static void Init(IEntitiesContextStorageContainer entitiesContextStorageContainer)
        {
            _entitiesContextStorageContainer = entitiesContextStorageContainer;
        }

        public static EntitiesContext GetEntitiesContext()
        {
            EntitiesContext entitiesContext = _entitiesContextStorageContainer.GetCurrentContext();
            if (entitiesContext == null)
            {
                entitiesContext = new EntitiesContext();
            }
            _entitiesContextStorageContainer.Store(entitiesContext);

            return entitiesContext;
        }

        public static void ResetEntitiesContent()
        {
            EntitiesContext entitiesContext = new EntitiesContext();
            _entitiesContextStorageContainer.Store(entitiesContext);
        }
    }
}
