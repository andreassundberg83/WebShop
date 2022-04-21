namespace WebShopAPI.Models.Forms
{
    public class PlaceOrderForm
    {
        public int UserId { get; set; }
        public List<OrderedProductForm> Cart { get; set; } = null!;        

    }
    public class UpdateOrderForm
    {
        public string Status { get; set; } = null!;
    }
}
