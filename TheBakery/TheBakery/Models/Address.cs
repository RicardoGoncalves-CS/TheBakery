namespace TheBakery.Models
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
    }
}
