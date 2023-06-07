using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.OrderDetailsDtos;

namespace TheBakery.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(bool, OrderDetails?)> CreateAsync(PostOrderDetailsDto orderDetailsDto)
        {
            if (_orderDetailsRepository.IsNull)
            {
                return (false, null);
            }

            var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);

            _orderDetailsRepository.Add(orderDetails);
            await _orderDetailsRepository.SaveAsync();

            return (true, orderDetails);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            if (_orderDetailsRepository.IsNull)
            {
                return new ServiceResult(false, "OrderDetails repository is not available.");
            }

            var orderDetails = await _orderDetailsRepository.FindAsync(id);

            if (orderDetails == null)
            {
                return new ServiceResult(false, "OrderDetails not found.");
            }

            _orderDetailsRepository.Remove(orderDetails);
            await _orderDetailsRepository.SaveAsync();

            return new ServiceResult(true, "OrderDetails deleted successfully.");
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return (await _orderDetailsRepository.FindAsync(id)) != null;
        }

        public async Task<IEnumerable<GetOrderDetailsDto>?> GetAllAsync()
        {
            if (_orderDetailsRepository.IsNull)
            {
                return null;
            }

            var orderDetails = await _orderDetailsRepository.GetAllAsync();

            var orderDetailsDto = _mapper.Map<List<GetOrderDetailsDto>>(orderDetails);

            foreach(var details in orderDetailsDto)
            {
                var product = await _productRepository.FindAsync(details.ProductId);
                if(product == null)
                {
                    return null;
                }

                details.Price = product.UnitPrice * details.Quantity;
            }

            return orderDetailsDto;
        }

        public async Task<GetOrderDetailsDto?> GetAsync(Guid id)
        {
            if (_orderDetailsRepository.IsNull)
            {
                return null;
            }

            var orderDetails = await _orderDetailsRepository.FindAsync(id);

            if (orderDetails == null)
            {
                return null;
            }

            var orderDetailsDto = _mapper.Map<GetOrderDetailsDto>(orderDetails);

            orderDetailsDto.Price = orderDetails.Product.UnitPrice * orderDetails.Quantity;

            return orderDetailsDto;
        }

        public async Task<Product> GetProductByOrderDetailsId(Guid id)
        {
            if (_orderDetailsRepository.IsNull)
            {
                return null;
            }

            var orderDetails = await _orderDetailsRepository.FindAsync(id);

            if (orderDetails == null)
            {
                return null;
            }

            var product = await _productRepository.FindAsync(orderDetails.ProductId);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, PutOrderDetailsDto orderDetailsDto)
        {
            var orderDetails = _mapper.Map<OrderDetails>(orderDetailsDto);
            
            _orderDetailsRepository.Update(orderDetails);

            try
            {
                await _orderDetailsRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return new ServiceResult(false, "OrderDetails update failed due to a concurrency conflict.");
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult(true, "OrderDetails updated successfully.");
        }
    }
}
