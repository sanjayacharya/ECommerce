using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrderService orderService,IProductService productService,ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(string customerId)
        {
            var orderResult= await _orderService.GetOrdersAsync(customerId);
            var productResult = await _productService.GetProductsAsync();
            var customerResult=await _customerService.GetCustomerAsync(customerId);
            if(orderResult.IsSuccess)
            {
                orderResult.Orders.ForEach(order =>
                {
                    order.CustomerName = customerResult.Customer.Name;
                    order.CustomerAddress = customerResult.Customer.Address;
                    order.Items.ForEach(item =>
                    {
                        item.ProductName = productResult.IsSuccess ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                        "Product information is not available";
                    });
                });

                var result = new
                {
                    Orders = orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
