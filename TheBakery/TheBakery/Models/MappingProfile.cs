using AutoMapper;
using TheBakery.Models.DTOs;

namespace TheBakery.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostAddressDto, Address>();
        }
    }
}
