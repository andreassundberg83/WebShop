using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public Product()
        {

        }

        public Product(ProductEntity productEntity)
        {
            Id = productEntity.Id;
            Name = productEntity.Name;
            Price = productEntity.Price;
            Description = productEntity.Description;
            CategoryName = productEntity.Category.Name != null ? productEntity.Category.Name : "";
            CategoryId = productEntity.Category.Id;
            ArticleNumber = productEntity.ArticleNumber;
        }

    }
}
