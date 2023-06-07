using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs;

namespace TheBakery.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<(bool, Address?)> CreateAsync(PostAddressDto addressDto)
        {
            if (_addressRepository.IsNull)
            {
                return (false, null);
            }

            var addressExists = await _addressRepository.FindAsync(
                addressDto.Number,
                addressDto.Street,
                addressDto.PostCode,
                addressDto.City);

            if (addressExists != null)
            {
                return (false, null);
            }

            var address = _mapper.Map<Address>(addressDto);

            _addressRepository.Add(address);
            await _addressRepository.SaveAsync();

            return (true, address);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (_addressRepository.IsNull)
            {
                return false;
            }

            var address = await _addressRepository.FindAsync(id);

            if (address == null)
            {
                return false;
            }

            _addressRepository.Remove(address);
            await _addressRepository.SaveAsync();

            return true;
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return (await _addressRepository.FindAsync(id)) != null;
        }

        public async Task<IEnumerable<Address>?> GetAllAsync()
        {
            if (_addressRepository.IsNull)
            {
                return null;
            }

            var addresses = await _addressRepository.GetAllAsync();

            return addresses;
        }

        public async Task<Address?> GetAsync(Guid id)
        {
            if (_addressRepository.IsNull)
            {
                return null;
            }

            var address = await _addressRepository.FindAsync(id);

            if (address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<Address?> GetByProperties(string number, string street, string postCode, string city)
        {
            if (_addressRepository.IsNull)
            {
                return null;
            }

            var address = await _addressRepository.FindAsync(number, street, postCode, city);

            if (address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, Address address)
        {
            var addressExists = await _addressRepository.FindAsync(
                address.Number,
                address.Street,
                address.PostCode,
                address.City);

            if (addressExists == null)
            {
                return new ServiceResult(false, "Address not found.");
            }

            if (addressExists != null && 
                addressExists.AddressId != address.AddressId)
            {
                return new ServiceResult(false, "Address with the same properties already exists.");
            }

            _addressRepository.Update(address);

            try
            {
                await _addressRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return new ServiceResult(false, "Address not found.");
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult(true, "Address updated successfully.");
        }
    }
}
