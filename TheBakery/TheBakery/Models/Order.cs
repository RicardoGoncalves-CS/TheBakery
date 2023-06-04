namespace TheBakery.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}