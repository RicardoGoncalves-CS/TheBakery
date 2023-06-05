namespace TheBakery.Models.DTOs.Customer
{
    public class GetCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public Guid? AddressId { get; set; }
    }
}
