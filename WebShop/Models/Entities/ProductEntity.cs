using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Models.Entities
{
    public class ProductEntity
    {        
        [Key]
        public int Id { get; set; }

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
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; } = null!;
        
        [Required]
        public int CategoryId { get; set; }
        
        public virtual CategoryEntity Category { get; set; } = null!;
        public ProductEntity()
        {

        }

        public ProductEntity(ProductForm form)
        {
            Name = form.Name;
            Description = form.Description;            
            Price = form.Price;
            ArticleNumber = form.ArticleNumber;
        }
    }
}
