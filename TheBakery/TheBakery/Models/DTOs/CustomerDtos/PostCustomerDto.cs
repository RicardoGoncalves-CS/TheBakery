namespace TheBakery.Models.DTOs.Customer
{
    public class PostCustomerDto
    {
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public PostAddressDto? Address { get; set; }
    }
}
