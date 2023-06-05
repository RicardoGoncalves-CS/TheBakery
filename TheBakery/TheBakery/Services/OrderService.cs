using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Models.DTOs.OrderDtos;

namespace TheBakery.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(bool, Order?)> CreateAsync(PostOrderDto orderDto)
        {
            if (_orderRepository.IsNull)
            {
                return (false, null);
            }

            var order = _mapper.Map<Order>(orderDto);

            _orderRepository.Add(order);
            await _orderRepository.SaveAsync();

            return (true, order);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (_orderRepository.IsNull)
            {
                return false;
            }

            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            _orderRepository.Remove(order);
            await _orderRepository.SaveAsync();

            return true;
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return (await _orderRepository.FindAsync(id)) != null;
        }

        public async Task<IEnumerable<GetOrderDto>?> GetAllAsync()
        {
            if (_orderRepository.IsNull)
            {
                return null;
            }

            var orders = await _orderRepository.GetAllAsync();

            var ordersDto = _mapper.Map<List<GetOrderDto>>(orders);

            foreach(var orderDto in ordersDto)
            {
                decimal total = 0;

                foreach (var orderDetails in orderDto.OrderDetails)
                {
                    var product = await _productRepository.FindAsync(orderDetails.ProductId);

                    if (product == null)
                    {
                        return null;
                    }

                    total += product.UnitPrice * orderDetails.Quantity;
                }

                orderDto.TotalPrice = total;
            }

            return ordersDto;
        }

        public async Task<GetOrderDto?> GetAsync(Guid id)
        {
            if (_orderRepository.IsNull)
            {
                return null;
            }

            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return null;
            }

            var orderDto = _mapper.Map<GetOrderDto>(order);

            decimal total = 0;

            foreach(var orderDetails in orderDto.OrderDetails)
            {
                var product = await _productRepository.FindAsync(orderDetails.ProductId);

                if(product == null)
                {
                    return null;
                }

                total += product.UnitPrice * orderDetails.Quantity;
            }

            orderDto.TotalPrice = total;

            return orderDto;
        }

        public async Task<bool> UpdateAsync(Guid id, PutOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            _orderRepository.Update(order);

            try
            {
                await _orderRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}
