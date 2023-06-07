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

        public async Task<(bool, Customer?)> CreateAsync(PostCustomerDto customerDto)
        {
            if (_customerRepository.IsNull)
            {
                return (false, null);
            }

            var customer = _mapper.Map<Customer>(customerDto);

            if (customer.Address != null)
            {
                var existingAddress = await _addressRepository.FindAsync(
                    customerDto.Address.Number, 
                    customerDto.Address.Street,
                    customerDto.Address.PostCode,
                    customerDto.Address.City);

                customer.Address = existingAddress ?? new Address
                {
                    Number = customer.Address.Number,
                    Street = customer.Address.Street,
                    PostCode = customer.Address.PostCode,
                    City = customer.Address.City
                };
            }

            _customerRepository.Add(customer);
            await _customerRepository.SaveAsync();

            return (true, customer);
        }

        public async Task<ServiceResult> DeleteAsync(Guid id)
        {
            if (_customerRepository.IsNull)
            {
                return new ServiceResult(false, "Customer repository is not available.");
            }

            var cutomer = await _customerRepository.FindAsync(id);

            if (cutomer == null)
            {
                return new ServiceResult(false, "Customer not found.");
            }

            _customerRepository.Remove(cutomer);
            await _customerRepository.SaveAsync();

            return new ServiceResult(true, "Customer deleted successfully.");
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

            var address = await _addressRepository.FindAsync(
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

            var cutomers = await _customerRepository.GetAllAsync();

            return _mapper.Map<List<GetCustomerDto>>(cutomers);
        }

        public async Task<GetCustomerDto?> GetAsync(Guid id)
        {
            if (_customerRepository.IsNull)
            {
                return null;
            }

            var cutomer = await _customerRepository.FindAsync(id);

            if (cutomer == null)
            {
                return null;
            }

            return _mapper.Map<GetCustomerDto>(cutomer);
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, PutCustomerDto entity)
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
                    return new ServiceResult(false, "Customer update failed due to a concurrency conflict.");
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult(true, "Customer updated successfully.");
        }
    }
}
