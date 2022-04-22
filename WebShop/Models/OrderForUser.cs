using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models
{
    public class OrderForUser
    {
        public int Id { get; set; }
        public string Created { get; set; } = null!;
        public string Updated { get; private set; } = null!;
        public string UserName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public List<OrderedProduct> Cart { get; set; } = new List<OrderedProduct>();
        public decimal Sum { get; set; }
       
        public OrderForUser()
        {

        }
        public OrderForUser(OrderEntity orderEntity)
        {
            Id = orderEntity.Id;
            Created = orderEntity.Created.ToString("dd MMMM yyyy HH:mm");   
            Updated = orderEntity.Updated.ToString("dd MMMM yyyy HH:mm");
            Status = orderEntity.Status;
            UserName = orderEntity.UserEmail;        
            
            
        }
    }

}
