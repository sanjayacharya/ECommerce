using System.Diagnostics.CodeAnalysis;

namespace ECommerce.Api.Users.Models
{
    public class User
    {
        public string Id { get; set; }
        [AllowNull]
        public string UserId { get; set; }
        [AllowNull]
        public string Password { get; set; }
        [AllowNull]
        public string Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
