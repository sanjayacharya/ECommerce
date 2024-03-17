using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Api.Users.Interfaces
{
    public interface IUserProvider
    {
        Task<(bool IsSuccess, List<Models.User> Users, string ErrorMessage)> GetUsersAsync();
        Task<(bool IsSuccess, Models.User User, string ErrorMessage)> GetUserAsync(string id);
        Task<(bool IsSuccess, Models.User User, string ErrorMessage)> GetLoginUserAsync(string userId,String password);
        Task<(bool IsSuccess, string ErrorMessage)> CreateUserAsync(Models.User model);
        Task<(bool IsSuccess, Models.User User, string ErrorMessage)> UpdateUserAsync(string id,Models.User model);
        Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(string id);
    }
}
