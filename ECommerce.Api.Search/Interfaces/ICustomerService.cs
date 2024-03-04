﻿using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool IsSuccess,Customer Customer,string ErrorMessage)> GetCustomerAsync(string customerId); 
    }
}
