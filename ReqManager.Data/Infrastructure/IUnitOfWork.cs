namespace ReqManager.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void SaveChanges();
    }
}
