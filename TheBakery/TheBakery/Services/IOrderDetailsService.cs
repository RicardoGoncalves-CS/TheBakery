using TheBakery.Models;
using TheBakery.Models.DTOs.OrderDetailsDtos;

namespace TheBakery.Services
{
    public interface IOrderDetailsService : IBakeryService<OrderDetails, GetOrderDetailsDto, PostOrderDetailsDto, PutOrderDetailsDto>
    {
        Task<Product> GetProductByOrderDetailsId(Guid id);
    }
}