using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models.Forms
{
    public class ProductForm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string ArticleNumber { get; set; } 
    }
}
    
