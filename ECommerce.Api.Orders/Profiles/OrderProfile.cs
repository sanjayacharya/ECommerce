﻿using AutoMapper;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.Order>();
            CreateMap<Db.OrderItem, Models.OrderItem>();
            CreateMap<Models.Order, Db.Order>();
            CreateMap<Models.OrderItem, Db.OrderItem>();
        }
    }
}
