using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;

namespace TheBakery.Services
{
    public interface ICustomerService : IBakeryService<Customer, GetCustomerDto, PostCustomerDto, PutCustomerDto>
    {
        Task<Address?> GetAddressByCustomerId(Guid id);
    }
}