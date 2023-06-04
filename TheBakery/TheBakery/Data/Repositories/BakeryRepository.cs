using Microsoft.EntityFrameworkCore;

namespace TheBakery.Data.Repositories
{
    public class BakeryRepository<T> : IBakeryRepository<T> where T : class
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<T> _dbSet;
        public BakeryRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task<T?> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
