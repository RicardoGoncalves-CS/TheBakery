namespace TheBakery.Models
{
    public class OrderDetails
    {
        public Guid OrderDetailsId { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}