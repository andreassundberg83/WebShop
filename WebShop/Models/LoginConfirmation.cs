namespace WebShopAPI.Models
{
    public class LoginConfirmation
    {
        public int UserId { get; set; }
        public string Jwt { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
