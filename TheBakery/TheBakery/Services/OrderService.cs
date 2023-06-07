using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Models.DTOs.OrderDetailsDtos;
using TheBakery.Models.DTOs.OrderDtos;

namespace TheBakery.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IOrderDetailsRepository orderDetailsRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _mapper = mapper;
        }

        public async Task<(bool, Order?)> CreateAsync(PostOrderDto orderDto)
        {
            if (_orderRepository.IsNull)
            {
                return (false, null);
            }

            var customer = await _customerRepository.FindIncludeAddressAsync(orderDto.CustomerId);

            if(customer == null || customer.AddressId == null)
            {
                return (false, null);
            }

            var order = _mapper.Map<Order>(orderDto);

            _orderRepository.Add(order);
            await _orderRepository.SaveAsync();

            return (true, order);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            if (_orderRepository.IsNull)
            {
                return new ServiceResult(false, "Order repository is not available.");
            }

            var order = await _orderRepository.FindAsync(id);

            if (order == null)
            {
                return new ServiceResult(false, "Order not found.");
            }

            _orderRepository.Remove(order);
            await _orderRepository.SaveAsync();

            return new ServiceResult(true, "Order deleted successfully.");
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
            var ordersDto = new List<GetOrderDto>();

            foreach (var order in orders)
            {
                var orderDto = _mapper.Map<GetOrderDto>(order);
                var orderDetailsDto = new List<GetLimitedOrderDetailsDto>();
                decimal total = 0;

                foreach (var orderDetails in order.OrderDetails)
                {
                    var product = await _productRepository.FindAsync(orderDetails.ProductId);

                    if (product == null)
                    {
                        return null;
                    }

                    var limitedOrderDetailsDto = new GetLimitedOrderDetailsDto
                    {
                        OrderDetailsId = orderDetails.OrderDetailsId,
                        Price = product.UnitPrice * orderDetails.Quantity
                    };

                    orderDetailsDto.Add(limitedOrderDetailsDto);
                    total += limitedOrderDetailsDto.Price;
                }

                orderDto.OrderDetails = orderDetailsDto;
                orderDto.TotalPrice = total;

                ordersDto.Add(orderDto);
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

            var orderDetailsDto = _mapper.Map<List<GetOrderDetailsDto>>(order.OrderDetails);
            var orderDto = _mapper.Map<GetOrderDto>(order);

            decimal total = 0;

            foreach(var orderDetails in orderDetailsDto)
            {
                var product = await _productRepository.FindAsync(orderDetails.ProductId);

                if(product == null)
                {
                    return null;
                }

                orderDetails.Price = product.UnitPrice * orderDetails.Quantity;
                total += product.UnitPrice * orderDetails.Quantity;
            }

            orderDto.OrderDetails = _mapper.Map<List<GetLimitedOrderDetailsDto>>(orderDetailsDto);
            orderDto.TotalPrice = total;

            return orderDto;
        }

        public async Task<GetCustomerDto> GetCustomerByOrderId(Guid id)
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

            var customer = await _customerRepository.FindAsync(order.CustomerId);

            return _mapper.Map<GetCustomerDto>(customer);
        }

        public async Task<List<GetOrderDetailsDto>> GetOrderDetailsByOrderId(Guid id)
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

            var orderDetails = _mapper.Map<List<GetOrderDetailsDto>>(order.OrderDetails);

            foreach(var details in orderDetails)
            {
                var product = await _productRepository.FindAsync(details.ProductId);

                if (product == null)
                {
                    return null;
                }

                details.Price = product.UnitPrice * details.Quantity;
            }

            return orderDetails;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, PutOrderDto orderDto)
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
                    return new ServiceResult(false, "Order update failed due to a concurrency conflict.");
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult(true, "Order updated successfully.");
        }
    }
}
