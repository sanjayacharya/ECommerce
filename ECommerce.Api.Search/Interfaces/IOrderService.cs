using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool IsSuccess, List<Order> Orders, string ErrorMessage)> GetOrdersAsync(string customerId);
    }
}
