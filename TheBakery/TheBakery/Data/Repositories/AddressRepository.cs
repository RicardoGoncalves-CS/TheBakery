using Microsoft.EntityFrameworkCore;
using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly TheBakeryContext _context;
        private readonly DbSet<Address> _dbSet;
        public AddressRepository(TheBakeryContext context)
        {
            _context = context;
            _dbSet = _context.Set<Address>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(Address address)
        {
            _dbSet.Add(address);
        }

        public void AddRange(IEnumerable<Address> address)
        {
            _dbSet.AddRange(address);
        }

        public async Task<Address?> FindAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Address?> FindAsync(string number, string street, string postCode, string city)
        {
            return await _dbSet.FirstOrDefaultAsync(a =>
                a.Number == number &&
                a.Street == street &&
                a.PostCode == postCode &&
                a.City == city);
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(Address address)
        {
            _dbSet.Remove(address);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Address address)
        {
            _dbSet.Update(address);
        }
    }
}
