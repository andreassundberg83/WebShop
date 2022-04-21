using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public User()
        {

        }
        
        public User(UserEntity userEntity)
        {
            Id = userEntity.Id;
            FirstName = userEntity.FirstName;
            LastName = userEntity.LastName;
            Email = userEntity.Email;
            Address = userEntity.Address;
            PostalCode = userEntity.PostalCode;
            City = userEntity.City;
        }

    }
}
