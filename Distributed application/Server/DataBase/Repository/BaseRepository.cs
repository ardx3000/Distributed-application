using Microsoft.EntityFrameworkCore;

namespace Server.DataBase.Repository
{
    public class BaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
