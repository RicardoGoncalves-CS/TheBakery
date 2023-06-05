using AutoMapper;
using TheBakery.Models.DTOs;
using TheBakery.Models.DTOs.Customer;

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
        }
    }
}
