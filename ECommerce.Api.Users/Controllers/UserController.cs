using ECommerce.Api.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Users.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        public UserController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            try
            {
                var currentUser = HttpContext.User;
                if (currentUser.HasClaim(c => c.Type == "IsActive"))
                {
                    var isActive = bool.Parse(currentUser.Claims.FirstOrDefault(s => s.Type == "IsActive").Value);
                    if (!isActive)
                        return BadRequest("User is not Active");
                }
              
                var result = await _userProvider.GetUsersAsync();
                if(result.IsSuccess)
                {
                    return Ok(result.Users);
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(string id)
        {
            try
            {
                var result = await _userProvider.GetUserAsync(id);
                if (result.IsSuccess)
                {
                    return Ok(result.User);
                }
                return NotFound(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(Models.User userModel)
        {
            try
            {
                if (userModel != null)
                {
                    var result = await _userProvider.CreateUserAsync(userModel);
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
        public async Task<IActionResult> UpdateUserAsync(string id,Models.User userModel)
        {
            try
            {
                if (userModel != null)
                {
                    var result = await _userProvider.UpdateUserAsync(id,userModel);
                    if (result.IsSuccess)
                    {
                        return Ok(result.User);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            try
            {
                var result = await _userProvider.DeleteUserAsync(id);
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
