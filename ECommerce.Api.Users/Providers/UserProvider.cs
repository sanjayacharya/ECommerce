using AutoMapper;
using ECommerce.Api.Users.Db;
using ECommerce.Api.Users.Interfaces;
using ECommerce.Api.Users.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Api.Users.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly UserDbContext _dbContext;
        private readonly ILogger<UserProvider> _logger;
        private readonly IMapper _mapper;
        public UserProvider(ILogger<UserProvider> logger,UserDbContext dbContext,IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> CreateUserAsync(Models.User model)
        {
            try
            {
                if (model != null)
                {
                    var editModel=_mapper.Map<Db.User>(model);
                    editModel.Id = Guid.NewGuid().ToString();
                    editModel.IsActive = true;
                    await _dbContext.Users.AddAsync(editModel);
                    await _dbContext.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Invalid Model");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUserAsync(string id)
        {
            try
            {
                var currentUser= await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (currentUser != null)
                {
                    _dbContext.Users.Remove(currentUser);
                    await _dbContext.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "User not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.User User, string ErrorMessage)> GetLoginUserAsync(string userId, string password)
        {
            try
            {
                var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId && x.Password == password);
                if (currentUser != null)
                {
                    var result = _mapper.Map<Models.User>(currentUser);
                    return (true, result, null);
                }
                return (false, null, "User not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.User User, string ErrorMessage)> GetUserAsync(string id)
        {
            try
            {
                var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (currentUser != null)
                {
                    var result = _mapper.Map<Models.User>(currentUser);
                    return (true, result, null);
                }
                return (false,null, "User not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<Models.User> Users, string ErrorMessage)> GetUsersAsync()
        {
            try
            {
                var currentUsers = await _dbContext.Users.ToListAsync();
                var results = _mapper.Map<List<Models.User>>(currentUsers);
                return (true, results, null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.User User, string ErrorMessage)> UpdateUserAsync(string id, Models.User model)
        {
            try
            {
                if (model != null)
                {
                    var currentUser = await _dbContext.Users.FirstOrDefaultAsync(s => s.IsActive ?? true && s.Id == id);
                    if (currentUser != null)
                    {
                        if(!string.IsNullOrEmpty(model.UserId))
                            currentUser.UserId = model.UserId;
                        if (!string.IsNullOrEmpty(model.Password))
                            currentUser.UserId = model.Password;
                        if (!string.IsNullOrEmpty(model.Email))
                            currentUser.UserId = model.Email;
                        if (model.IsActive.HasValue)
                            currentUser.IsActive = model.IsActive;
                        _dbContext.Users.Update(currentUser);
                        await _dbContext.SaveChangesAsync();
                        currentUser= await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
                        var updatedUser= _mapper.Map<Models.User>(currentUser);
                        return (true, updatedUser, null);
                    }
                    return (false, null, "User not found");
                }
                return (false,null, "Invalid Model");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }
    }
}
