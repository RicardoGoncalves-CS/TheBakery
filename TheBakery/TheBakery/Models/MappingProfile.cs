using AutoMapper;
using TheBakery.Models.DTOs;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Models.DTOs.OrderDetailsDtos;
using TheBakery.Models.DTOs.Product;

namespace TheBakery.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Address mapping
            CreateMap<PostAddressDto, Address>();

            // Customer mapping
            CreateMap<Customer, GetCustomerDto>();
            CreateMap<PostCustomerDto, Customer>();
            CreateMap<PutCustomerDto, Customer>();

            // Product mapping
            CreateMap<PostProductDto, Product>();

            // OrderDetails mapping
            CreateMap<PostOrderDetailsDto, OrderDetails>();
            CreateMap<PutOrderDetailsDto, OrderDetails>();
            CreateMap<OrderDetails, GetOrderDetailsDto>();
        }
    }
}
