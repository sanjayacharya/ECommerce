using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderProvider> _logger;
        public OrderProvider(OrderDbContext dbContext,IMapper mapper,ILogger<OrderProvider> logger) 
        { 
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> CreateOrderAsync(Models.Order order)
        {
            try
            {
                if (order == null) { throw new Exception("Order details required"); }
                var editModel = _mapper.Map<Db.Order>(order);
                var orderId = Guid.NewGuid().ToString();
                editModel.Id = orderId;
                editModel.Items.ForEach(item => item.OrderId = orderId);
                await _dbContext.Orders.AddAsync(editModel);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteOrderAsync(string id)
        {
            try
            {               
                var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(s => s.Id == id);
                if (existingOrder == null) { throw new Exception("Order Not Found"); }
                _dbContext.Orders.Remove(existingOrder);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> GetOrderAsync(string id)
        {
            try
            {
                var order = await _dbContext.Orders.Where(s => s.Id == id).Include(s=>s.Items).FirstOrDefaultAsync();
                if (order == null)
                    throw new Exception("Order Not Found");
                var result = _mapper.Map<Models.Order>(order);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await _dbContext.Orders.Select(s => s).Include(s => s.Items).ToListAsync();
                var result = _mapper.Map<List<Models.Order>>(orders);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<Models.Order> Orders, string ErrorMessage)> GetOrdersByCustomerIdAsync(string customerId)
        {
            try
            {
                var orders = await _dbContext.Orders.Where(s => s.CustomerId == customerId).Include(s => s.Items).ToListAsync();
                var result = _mapper.Map<List<Models.Order>>(orders);
                return (true, result, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Order Order, string ErrorMessage)> UpdateOrderAsync(string id, Models.Order order)
        {
            try
            {
                if (order == null) { throw new Exception("Order details required"); }
                var existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(s => s.Id == id);
                if(existingOrder == null) { throw new Exception("Order Not Found"); }
                var editModel = _mapper.Map<Db.Order>(order);
                editModel.Id = id;
                editModel.Items.ForEach(item => item.OrderId = id);
                _dbContext.Orders.Update(editModel);
                await _dbContext.SaveChangesAsync();
                existingOrder = await _dbContext.Orders.FirstOrDefaultAsync(s => s.Id == id);
                var updatedOrder= _mapper.Map<Models.Order>(existingOrder);
                return (true, updatedOrder, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }
    }
}
