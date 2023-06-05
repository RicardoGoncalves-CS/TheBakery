namespace TheBakery.Models.DTOs.OrderDetailsDtos
{
    public class GetOrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}