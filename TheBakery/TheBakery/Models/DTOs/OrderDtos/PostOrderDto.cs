using TheBakery.Models.DTOs.OrderDetailsDtos;

namespace TheBakery.Models.DTOs.OrderDtos
{
    public class PostOrderDto
    {
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<PostOrderDetailsDto> OrderDetails { get; set; }
    }
}
