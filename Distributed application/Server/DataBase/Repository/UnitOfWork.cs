using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Server.DataBase.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Logs = new LogsRepository(_context);
            Items = new ItemRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public ILogsRepository Logs { get; private set; }
        public IItemRepository Items { get; private set; }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Log the exception details for more information
                Console.WriteLine($"Update error: {ex.InnerException?.Message}");
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
