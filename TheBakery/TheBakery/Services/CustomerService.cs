using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.Customer;

namespace TheBakery.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, IAddressRepository addressRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<(bool, Customer?)> CreateAsync(PostCustomerDto entity)
        {
            if (_customerRepository.IsNull)
            {
                return (false, null);
            }

            var customer = _mapper.Map<Customer>(entity);

            _customerRepository.Add(customer);
            await _customerRepository.SaveAsync();

            return (true, customer);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (_customerRepository.IsNull)
            {
                return false;
            }

            var entity = await _customerRepository.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _customerRepository.Remove(entity);
            await _customerRepository.SaveAsync();

            return true;
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return (await _customerRepository.FindAsync(id)) != null;
        }

        public async Task<Address?> GetAddressByCustomerId(Guid id)
        {
            if (_customerRepository.IsNull)
            {
                return null;
            }

            var customer = await _customerRepository.FindIncludeAddressAsync(id);

            if (customer == null)
            {
                return null;
            }

            var address = await _addressRepository
                .FindAsync(
                    customer.Address.Number, 
                    customer.Address.Street, 
                    customer.Address.PostCode, 
                    customer.Address.City);

            if(address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<IEnumerable<GetCustomerDto>?> GetAllAsync()
        {
            if (_customerRepository.IsNull)
            {
                return null;
            }

            var entities = await _customerRepository.GetAllAsync();

            return _mapper.Map<List<GetCustomerDto>>(entities);
        }

        public async Task<GetCustomerDto?> GetAsync(Guid id)
        {
            if (_customerRepository.IsNull)
            {
                return null;
            }

            var entity = await _customerRepository.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<GetCustomerDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, PutCustomerDto entity)
        {
            var customer = _mapper.Map<Customer>(entity);

            if (entity.Address != null)
            {
                var existingAddress = await _addressRepository.FindAsync(entity.Address.Number, entity.Address.Street, entity.Address.PostCode, entity.Address.City);
                if (existingAddress != null)
                {
                    customer.Address = existingAddress;
                }
            }

            _customerRepository.Update(customer);

            try
            {
                await _customerRepository.SaveAsync();
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
