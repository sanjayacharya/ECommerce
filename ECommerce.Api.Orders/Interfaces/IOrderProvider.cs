namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, List<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccess, List<Models.Order> Orders, string ErrorMessage)> GetOrdersByCustomerIdAsync(string customerId);
        Task<(bool IsSuccess,Models.Order Order, string ErrorMessage)> GetOrderAsync(string id);
        Task<(bool IsSuccess, string ErrorMessage)> CreateOrderAsync(Models.Order order);
        Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> UpdateOrderAsync(string id, Models.Order order);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteOrderAsync(string id);
    }
}
