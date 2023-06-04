using System.Reflection.Metadata.Ecma335;

namespace TheBakery.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}