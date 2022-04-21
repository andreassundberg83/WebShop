namespace WebShopAPI.Models.Forms
{
    public class SignUpForm
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;   
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
