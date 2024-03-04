using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductProvider : IProductProvider
    {
        private readonly ProductDbContext _dbContext;
        private readonly ILogger<ProductProvider> _logger;
        private readonly IMapper _mapper;
        public ProductProvider(ProductDbContext dbContext, ILogger<ProductProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> CreateProductAsync(Models.Product product)
        {
            try
            {
                if(product != null)
                {
                    var editModel= _mapper.Map<Db.Product>(product);
                    editModel.Id=Guid.NewGuid().ToString();
                    await _dbContext.Products.AddAsync(editModel);
                    await _dbContext.SaveChangesAsync();
                    return (true, null);
                }
                return (false, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(string id)
        {
            try
            {
                var currentProduct = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == id);
                if (currentProduct != null)
                {
                    _dbContext.Remove(currentProduct);
                    await _dbContext.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Recortd Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(string id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == id);
                if(product != null)
                {
                    var result = _mapper.Map<Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<List<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> UpdateProductAsync(string id, Models.Product product)
        {
            try
            {
                var currentProduct = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == id);
                if (currentProduct != null)
                {
                    currentProduct.Name = product.Name;
                    currentProduct.Price = product.Price;
                    currentProduct.Inventory = product.Inventory;
                    _dbContext.Update(currentProduct);
                    await _dbContext.SaveChangesAsync();
                    var result = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == id);
                    var updatedProduct = _mapper.Map<Models.Product>(result);
                    return (true, updatedProduct, null);
                }
                return (false, null, "Recortd Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
