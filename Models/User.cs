namespace NextEcommerceWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserRole { get; set; } = "customer";
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
