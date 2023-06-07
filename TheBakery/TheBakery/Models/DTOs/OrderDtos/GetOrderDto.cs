using TheBakery.Models.DTOs.OrderDetailsDtos;

namespace TheBakery.Models.DTOs.OrderDtos
{
    public class GetOrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<GetLimitedOrderDetailsDto> OrderDetails { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
