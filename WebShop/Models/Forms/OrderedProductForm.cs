using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebShopAPI.Models.Forms
{    
    public class OrderedProductForm 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
