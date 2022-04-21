
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Models.Entities
{
    public class OrderedProductEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int OrderId { get; set; }

        public OrderEntity Order { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ArticleNumber { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = null!;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }        

        [Required]
        public int Quantity { get; set; }
        public OrderedProductEntity()
        {

        }
        public OrderedProductEntity(int orderId, ProductEntity input , int quantity)
        {            
            Quantity = quantity;
            OrderId = orderId;
            ArticleNumber = input.ArticleNumber;
            Name = input.Name;
            Price = input.Price;
        }
    }
    
}
