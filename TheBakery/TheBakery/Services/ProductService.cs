using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheBakery.Data.Repositories;
using TheBakery.Models;
using TheBakery.Models.DTOs.Product;

namespace TheBakery.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(bool, Product?)> CreateAsync(PostProductDto productDto)
        {
            if (_productRepository.IsNull)
            {
                return (false, null);
            }

            var product = _mapper.Map<Product>(productDto);

            _productRepository.Add(product);
            await _productRepository.SaveAsync();

            return (true, product);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            if (_productRepository.IsNull)
            {
                return false;
            }

            var product = await _productRepository.FindAsync(id);

            if (product == null)
            {
                return false;
            }

            _productRepository.Remove(product);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<bool> EntityExists(Guid id)
        {
            return (await _productRepository.FindAsync(id)) != null;
        }

        public async Task<IEnumerable<Product>?> GetAllAsync()
        {
            if (_productRepository.IsNull)
            {
                return null;
            }

            var products = await _productRepository.GetAllAsync();

            return products;
        }

        public async Task<Product?> GetAsync(Guid id)
        {
            if (_productRepository.IsNull)
            {
                return null;
            }

            var product = await _productRepository.FindAsync(id);

            if (product == null)
            {
                return null;
            }

            return product;
        }

        public async Task<ServiceResult> UpdateAsync(Guid id, Product product)
        {
            _productRepository.Update(product);

            try
            {
                await _productRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return new ServiceResult(false, "");
                }
                else
                {
                    throw;
                }
            }

            return new ServiceResult(true, "");
        }
    }
}
