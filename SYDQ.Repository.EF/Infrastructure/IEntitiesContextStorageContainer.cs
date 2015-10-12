namespace SYDQ.Repository.EF
{
    public interface IEntitiesContextStorageContainer
    {
        EntitiesContext GetCurrentContext();
        void Store(EntitiesContext dataContext);
    }
}
