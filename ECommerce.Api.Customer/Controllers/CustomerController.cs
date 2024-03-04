using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;
        public CustomerController(ICustomerProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            try
            {
                var result =await _customerProvider.GetCustomersAsync();
                if (result.IsSuccess)
                {
                    return Ok(result.Customers);
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerAsync(string id)
        {
            try
            {
                var result = await _customerProvider.GetCustomerAsync(id);
                if (result.IsSuccess)
                {
                    return Ok(result.Customer);
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(Models.Customer model)
        {
            try
            {
                var result = await _customerProvider.CreateCustomerAsync(model);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(string id,Models.Customer model)
        {
            try
            {
                var result = await _customerProvider.UpdateCustomerAsync(id, model);
                if (result.IsSuccess)
                {
                    return Ok(result.Customer);
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(string id)
        {
            try
            {
                var result = await _customerProvider.DeleteCustomerAsync(id);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
