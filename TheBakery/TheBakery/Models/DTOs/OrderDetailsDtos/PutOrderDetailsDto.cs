﻿namespace TheBakery.Models.DTOs.OrderDetailsDtos
{
    public class PutOrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
