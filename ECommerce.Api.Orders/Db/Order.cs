namespace ECommerce.Api.Orders.Db
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
