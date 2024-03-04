namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, List<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(string id);
        Task<(bool IsSuccess, string ErrorMessage)> CreateCustomerAsync(Models.Customer model);
        Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> UpdateCustomerAsync(string id, Models.Customer model);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteCustomerAsync(string id);
    }
}
