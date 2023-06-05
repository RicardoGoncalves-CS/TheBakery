﻿using TheBakery.Models;
using TheBakery.Models.DTOs.OrderDtos;

namespace TheBakery.Services
{
    public interface IOrderService : IBakeryService<Order, GetOrderDto, PostOrderDto, PutOrderDto>
    {
    }
}