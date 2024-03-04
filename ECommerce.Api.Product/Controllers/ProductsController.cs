using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider _productProvider;
        public ProductsController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var result = await _productProvider.GetProductsAsync();
                if (result.IsSuccess)
                {
                    return Ok(result.Products);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var result = await _productProvider.GetProductAsync(id);
                if(result.IsSuccess)
                {
                    return Ok(result.Product);
                }
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProducAsync(Product productModel)
        {
            try
            {
                if (productModel != null)
                {
                    var result = await _productProvider.CreateProductAsync(productModel);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(string id,Product productModel)
        {
            try
            {
                if (productModel != null)
                {
                    var result = await _productProvider.UpdateProductAsync(id, productModel);
                    if(result.IsSuccess)
                    {
                        return Ok(result.Product);
                    }
                    return NotFound(result.ErrorMessage);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(string id)
        {
            try
            {
                var result = await _productProvider.DeleteProductAsync(id);
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
