using TheBakery.Models;
using TheBakery.Models.DTOs;

namespace TheBakery.Services
{
    public interface IAddressService : IBakeryService<Address, Address, PostAddressDto, Address>
    {
    }
}