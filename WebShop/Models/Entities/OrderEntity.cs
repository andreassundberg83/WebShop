using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Models.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime Created { get; set; }    
        
        public DateTime Updated { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserName { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserEmail { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string UserAddress { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; } = null!;

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string PostalCode { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; } = null!;        
        

        public virtual ICollection<OrderedProductEntity> Cart { get; set; } = null!;
        public OrderEntity()
        {

        }
        public OrderEntity(PlaceOrderForm form, UserEntity user)
        {
            UserId = form.UserId;            
            Status = "New order";            
            Created = DateTime.Now;
            Updated = DateTime.Now;
            UserName = $"{user.FirstName} {user.LastName}";
            UserAddress = user.Address;
            City = user.City;
            PostalCode = user.PostalCode;  
            UserEmail = user.Email;
            Cart = new List<OrderedProductEntity>();
        }

    }
}