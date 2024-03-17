namespace ECommerce.Api.Users.Db
{
    public class User
    {
        public User()
        {
           
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
