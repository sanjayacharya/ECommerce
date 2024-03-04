﻿namespace ECommerce.Api.Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
