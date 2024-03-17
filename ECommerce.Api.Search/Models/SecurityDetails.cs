namespace ECommerce.Api.Search.Models
{
    public class SecurityDetailsOptions
    {
        public const string SecurityDetails = "SecurityDetails";
        public bool? EnableSecurity { get; set; }
        public string? SecurityKey { get; set; }
        public string? Token { get; set; }
    }
}
