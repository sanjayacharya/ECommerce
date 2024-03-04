using ECommerce.Api.Products.Db;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess,List<Models.Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(string id);
        Task<(bool IsSuccess,string ErrorMessage)> CreateProductAsync(Models.Product product);
        Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> UpdateProductAsync(string id, Models.Product product);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteProductAsync(string id);
    }
}
