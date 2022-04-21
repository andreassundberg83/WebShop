namespace WebShopAPI.Models
{
    public class UserUpdateForm
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;       
        public string Password { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
