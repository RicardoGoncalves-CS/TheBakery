using TheBakery.Models;
using TheBakery.Models.DTOs.Product;

namespace TheBakery.Services
{
    public interface IProductService : IBakeryService<Product, Product, PostProductDto, Product>
    {
    }
}
