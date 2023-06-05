namespace TheBakery.Models.DTOs.OrderDetailsDtos
{
    public class GetOrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public TheBakery.Models.Product Product { get; set; }
        public int Quantity { get; set; }
    }
}