using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models
{
    public class OrderedProduct
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderedProduct()
        {

        }
        public OrderedProduct(OrderedProductEntity item)
        {
            ProductName = item.Name;
            Quantity = item.Quantity;
            Price = item.Price;

        }
    }
}
