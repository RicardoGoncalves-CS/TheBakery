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

        public async Task<(bool, Address?)> CreateAsync(PostAddressDto entity)
        {
            if (_addressRepository.IsNull)
            {
                return (false, null);
            }

            var address = _mapper.Map<Address>(entity);

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

            var entity = await _addressRepository.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            _addressRepository.Remove(entity);
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

            var entities = await _addressRepository.GetAllAsync();

            return entities;
        }

        public async Task<Address?> GetAsync(Guid id)
        {
            if (_addressRepository.IsNull)
            {
                return null;
            }

            var entity = await _addressRepository.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<Address?> GetByProperties(string number, string street, string postCode, string city)
        {
            if (_addressRepository.IsNull)
            {
                return null;
            }

            var entity = await _addressRepository.FindAsync(number, street, postCode, city);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> UpdateAsync(Guid id, Address entity)
        {
            _addressRepository.Update(entity);

            try
            {
                await _addressRepository.SaveAsync();
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
