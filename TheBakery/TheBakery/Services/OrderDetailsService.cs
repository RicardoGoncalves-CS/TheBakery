using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;
using TheBakery.Models.DTOs.OrderDetailsDtos;

namespace TheBakery.Services
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IMapper _mapper;

        public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository, IMapper mapper)
        {
            _orderDetailsRepository = orderDetailsRepository;
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

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (_orderDetailsRepository.IsNull)
            {
                return false;
            }

            var orderDetails = await _orderDetailsRepository.FindAsync(id);

            if (orderDetails == null)
            {
                return false;
            }

            _orderDetailsRepository.Remove(orderDetails);
            await _orderDetailsRepository.SaveAsync();

            return true;
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

            return _mapper.Map<List<GetOrderDetailsDto>>(orderDetails);
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

            return _mapper.Map<GetOrderDetailsDto>(orderDetails);
        }

        public async Task<bool> UpdateAsync(Guid id, PutOrderDetailsDto orderDetailsDto)
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
