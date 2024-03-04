using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool IsSuccess, List<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
