namespace SYDQ.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Commit();
        void Rollback();
    }
}
