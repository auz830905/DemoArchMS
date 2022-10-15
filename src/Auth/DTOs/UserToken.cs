namespace Auth.DTOs
{
    public class UserToken
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}