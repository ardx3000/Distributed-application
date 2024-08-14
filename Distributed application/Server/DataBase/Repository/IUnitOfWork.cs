namespace Server.DataBase.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ILogsRepository Logs { get; }
        IItemRepository Items { get; }
        int Complete();
    }
}
