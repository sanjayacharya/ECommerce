using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider _orderProvider;
        public OrdersController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            try
            {
                var result= await _orderProvider.GetOrdersAsync();
                if(result.IsSuccess)
                    return Ok(result.Orders);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(string id)
        {
            try
            {
                var result = await _orderProvider.GetOrderAsync(id);
                if (result.IsSuccess)
                    return Ok(result.Order);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerIdAsync(string customerId)
        {
            try
            {
                var result = await _orderProvider.GetOrdersByCustomerIdAsync(customerId);
                if (result.IsSuccess)
                    return Ok(result.Orders);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(Models.Order order)
        {
            try
            {
                var result = await _orderProvider.CreateOrderAsync(order);
                if (result.IsSuccess)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> CreateOrderAsync(string id,Models.Order order)
        {
            try
            {
                var result = await _orderProvider.UpdateOrderAsync(id,order);
                if (result.IsSuccess)
                    return Ok(result.Order);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(string id)
        {
            try
            {
                var result = await _orderProvider.DeleteOrderAsync(id);
                if (result.IsSuccess)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
