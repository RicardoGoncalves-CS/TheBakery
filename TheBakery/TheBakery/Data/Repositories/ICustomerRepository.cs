using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public interface ICustomerRepository : IBakeryRepository<Customer>
    {
        Task<Customer?> FindIncludeAddressAsync(Guid id);
    }
}
