namespace ECommerce.Api.Search.Models
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
