using Microsoft.EntityFrameworkCore;
using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(Product product)
        {
            _dbSet.Add(product);
        }

        public void AddRange(IEnumerable<Product> product)
        {
            _dbSet.AddRange(product);
        }

        public async Task<Product?> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Product?> FindAsync(string productName)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(Product product)
        {
            _dbSet.Remove(product);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            _dbSet.Update(product);
        }
    }
}
