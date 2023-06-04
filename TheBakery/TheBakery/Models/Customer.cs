namespace TheBakery.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public Guid? AddressId { get; set; }
        public Address? Address { get; set; }
    }
}