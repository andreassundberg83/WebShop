using WebShopAPI.Models.Entities;

namespace WebShopAPI.Models
{
    public class OrderForAdmin
    {

        public int Id { get; set; }
        public string Created { get; set; }
        public string Updated { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Status { get; set; } = null!;
        public List<OrderedProduct> Cart { get; set; } = new List<OrderedProduct>();
        public decimal Sum { get; set; }

        public OrderForAdmin()
        {

        }
        public OrderForAdmin(OrderEntity orderEntity)
        {
            Id = orderEntity.Id;
            Created = orderEntity.Created.ToString("dd MMMM yyyy HH:mm");
            Updated = orderEntity.Updated.ToString("dd MMMM yyyy HH:mm");
            Status = orderEntity.Status;
            UserName = orderEntity.UserName;
            Email = orderEntity.UserEmail;
            Address = orderEntity.UserAddress;
            City = orderEntity.City;
            PostalCode = orderEntity.PostalCode;
            UserId = orderEntity.UserId;
        }
    }
}
