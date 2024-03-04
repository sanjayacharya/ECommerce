using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.Api.Customers.Provides
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerProvider> _logger;
        public CustomerProvider(CustomerDbContext dbContext, IMapper mapper, ILogger<CustomerProvider> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<(bool IsSuccess, string ErrorMessage)> CreateCustomerAsync(Models.Customer model)
        {
            try
            {
                if (model != null)
                {
                    var editModel=_mapper.Map<Customer>(model);
                    editModel.Id = Guid.NewGuid().ToString();
                    await _dbContext.AddAsync(editModel);
                    await _dbContext.SaveChangesAsync();
                    return (true, null);
                }
                return (false, "Input is not valid");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteCustomerAsync(string id)
        {
            try
            {
                var currentCustomer = await _dbContext.Customers.FirstOrDefaultAsync(s => s.Id == id);
                if (currentCustomer == null)
                {
                    throw new Exception("Customer not found");
                }
                _dbContext.Remove(currentCustomer);
                await _dbContext.SaveChangesAsync();
                return (true, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(string id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(s=>s.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<List<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer Customer, string ErrorMessage)> UpdateCustomerAsync(string id, Models.Customer model)
        {
            try
            {
                if (model != null)
                {
                    var currentCustomer = await _dbContext.Customers.FirstOrDefaultAsync(s => s.Id == id);
                    if (currentCustomer == null)
                    {
                        throw new Exception("Customer not found");
                    }
                    currentCustomer.Name=model.Name;
                    currentCustomer.Address=model.Address;
                    _dbContext.Update(currentCustomer);
                    await _dbContext.SaveChangesAsync();
                    currentCustomer = await _dbContext.Customers.FirstOrDefaultAsync(s => s.Id == id);
                    var updatedCustomer = _mapper.Map<Models.Customer>(currentCustomer);
                    return (true, updatedCustomer, null);
                }
                return (false, null,null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }
    }
}
