using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string PostalCode { get; set; } = null!;

        [Required]
        public byte[] PasswordHash { get; private set; } = null!;

        [Required]
        public byte[] Salt { get; private set; } = null!;

        [Required]
        public bool IsAdmin { get; set; }
        public UserEntity()
        {

        }
        public UserEntity(SignUpForm form)
        {
            FirstName = form.FirstName;
            LastName = form.LastName;
            Email = form.Email;
            Address = form.Address;
            City = form.City;
            PostalCode = form.PostalCode;
            IsAdmin = form.IsAdmin;
            CreateSecurePassword(form.Password);
        }


        public void CreateSecurePassword(string password) 
        {
            using (var hmac = new HMACSHA512())
            {
                Salt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool CompareSecurePassword(string password)
        {
            using (var hmac = new HMACSHA512(Salt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                return hash.SequenceEqual(PasswordHash);
                //for (int i = 0; i < hash.Length; i++)
                //{
                //    if (hash[i] != PasswordHash[i])
                //        return false;
                //}
            }

            //return true;
        }
    }

   
}
