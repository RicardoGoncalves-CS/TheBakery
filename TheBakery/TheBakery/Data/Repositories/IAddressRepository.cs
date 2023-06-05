using TheBakery.Models;

namespace TheBakery.Data.Repositories
{
    public interface IAddressRepository : IBakeryRepository<Address>
    {
        Task<Address?> FindAsync(string number, string street, string postCode, string city);
    }
}
