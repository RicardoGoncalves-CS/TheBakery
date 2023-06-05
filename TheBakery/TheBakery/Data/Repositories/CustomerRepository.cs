using Microsoft.EntityFrameworkCore;
using System.Net;
using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<Customer>();
        }
        public bool IsNull => _dbSet == null;

        public void Add(Customer customer)
        {
            _dbSet.Add(customer);
        }

        public void AddRange(IEnumerable<Customer> customers)
        {
            _dbSet.AddRange(customers);
        }

        public async Task<Customer?> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Customer?> FindIncludeAddressAsync(Guid id)
        {
            return await _dbSet.Include(c => c.Address).FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(Customer customer)
        {
            _dbSet.Remove(customer);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Customer customer)
        {
            _dbSet.Update(customer);
        }
    }
}
