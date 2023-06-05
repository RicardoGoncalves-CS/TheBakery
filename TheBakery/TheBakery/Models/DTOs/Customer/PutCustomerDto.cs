namespace TheBakery.Models.DTOs.Customer
{
    public class PutCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public PostAddressDto? Address { get; set; }
    }
}