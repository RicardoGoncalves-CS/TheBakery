using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<Order> _dbSet;

        public OrderRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<Order>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(Order order)
        {
            _dbSet.Add(order);
        }

        public void AddRange(IEnumerable<Order> orders)
        {
            _dbSet.AddRange(orders);
        }

        public async Task<Order?> FindAsync(Guid id)
        {
            return await _dbSet.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbSet.Include(o => o.OrderDetails).ToListAsync();
        }

        public void Remove(Order order)
        {
            _dbSet.Remove(order);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Order order)
        {
            _dbSet.Update(order);
        }
    }
}
