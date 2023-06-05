using Microsoft.EntityFrameworkCore;
using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<OrderDetails> _dbSet;
        public OrderDetailsRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<OrderDetails>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(OrderDetails orderDetails)
        {
            _dbSet.Add(orderDetails);
        }

        public void AddRange(IEnumerable<OrderDetails> orderDetails)
        {
            _dbSet.AddRange(orderDetails);
        }

        public async Task<OrderDetails?> FindAsync(Guid id)
        {
            return await _dbSet.Include(od => od.Product).FirstOrDefaultAsync(od => od.OrderDetailsId == id);
        }

        public async Task<IEnumerable<OrderDetails>> GetAllAsync()
        {
            return await _dbSet.Include(od => od.Product).ToListAsync();
        }

        public void Remove(OrderDetails orderDetails)
        {
            _dbSet.Remove(orderDetails);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(OrderDetails orderDetails)
        {
            _dbSet.Update(orderDetails);
        }
    }
}
