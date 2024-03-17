using ECommerce.Api.Users.Interfaces;
using ECommerce.Api.Users.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.Api.Users.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        private IConfiguration _configuration;  
        public LoginController(IUserProvider userProvider, IConfiguration configuration)
        {
            _userProvider = userProvider;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userProvider.GetLoginUserAsync(loginRequest.UserID, loginRequest.Password);
                if (user.IsSuccess)
                {
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:ScrectKey"]));
                    var credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Sub,user.User.UserId),
                    new Claim(JwtRegisteredClaimNames.Email,user.User.Email),
                    new Claim("IsActive",user.User.IsActive.ToString()),

                    };
                    var securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: credentials);

                    var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
