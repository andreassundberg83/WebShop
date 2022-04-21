using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopAPI.Models;
using WebShopAPI.Models.Data;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Services
{
    public interface IOrderService
    {
        Task<OrderForUser> CreateAsync(PlaceOrderForm form);
        Task<OrderForUser> UserGetByIdAsync(int orderId);
        Task<OrderForAdmin> AdminGetByIdAsync(int orderId);
        Task<IEnumerable<OrderForAdmin>> GetAllAsync();
        Task<OrderForAdmin> UpdateAsync(int id, UpdateOrderForm form);        
        Task<IEnumerable<OrderForUser>> GetAllOrdersByUserIdAsync(int userId);
        Task<bool> DeleteAsync(int id);
       
    }
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;        

        public OrderService(DataContext context)
        {
            _context = context;
        }

        #region Create
        public async Task<OrderForUser> CreateAsync(PlaceOrderForm form)
        {
            var userEntity = await _context.Users.FindAsync(form.UserId);
            var orderEntity = new OrderEntity(form, userEntity!);
            _context.Add(orderEntity);
            await _context.SaveChangesAsync();
            foreach (var item in form.Cart)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                orderEntity.Cart.Add(new OrderedProductEntity(orderEntity.Id, product, item.Quantity));
                await _context.SaveChangesAsync();
            }
            
            var returnOrder = new OrderForUser(orderEntity);

            foreach (var item in orderEntity.Cart)
            {
                returnOrder.Cart.Add(new OrderedProduct(item));
                returnOrder.Sum += item.Price * item.Quantity;
            }

            return returnOrder;            

        }
        #endregion  


        #region Read
        #region Admin        
        public async Task<IEnumerable<OrderForAdmin>> GetAllAsync()
        {
            var orders = new List<OrderForAdmin>();
            var orderEntities = await _context.Orders
                .Include(c => c.Cart)
                .ToListAsync();
            if (!orderEntities.Any()) return null!;
            foreach (var orderEntity in orderEntities)
            {
                var temporder = new OrderForAdmin(orderEntity);
                foreach (var product in orderEntity.Cart)
                {
                    temporder.Cart.Add(new OrderedProduct(product));
                    temporder.Sum += product.Price * product.Quantity;
                }
                orders.Add(temporder);
            }
            return orders;
        }
        public async Task<OrderForAdmin> AdminGetByIdAsync(int orderId)
        {
            var orderEntity = await _context.Orders
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(x => x.Id == orderId);
            if (orderEntity == null) return null!;
            var order = new OrderForAdmin(orderEntity);
            foreach (var item in orderEntity.Cart)
            {
                order.Cart.Add(new OrderedProduct(item));
            }
            return order;
        }
        #endregion
        #region User
        public async Task<IEnumerable<OrderForUser>> GetAllOrdersByUserIdAsync(int userId)
        {
            var orders = new List<OrderForUser>();
            var orderEntities =  await _context.Orders
                .Where(c => c.UserId == userId)
                .Include(c => c.Cart)
                .ToListAsync();
            if (!orderEntities.Any()) return null!;
            foreach (var orderEntity in orderEntities)
            {
                var temporder = new OrderForUser(orderEntity);
                foreach (var product in orderEntity.Cart)
                {
                    temporder.Cart.Add(new OrderedProduct(product));
                    temporder.Sum += product.Price * product.Quantity;
                }
                orders.Add(temporder);
            }
            return orders;
        }
        
        public async Task<OrderForUser> UserGetByIdAsync(int orderId)
        {
            var orderEntity = await _context.Orders
                .Include(c => c.Cart)
                .FirstOrDefaultAsync(x => x.Id == orderId);
            if (orderEntity == null) return null!;
            var order = new OrderForUser(orderEntity);
            foreach (var item in orderEntity.Cart)
            {
                order.Cart.Add(new OrderedProduct(item));
            }
            return order;
        }
        #endregion
        #endregion


        #region Update
        public async Task<OrderForAdmin> UpdateAsync(int id, UpdateOrderForm form)
        {
            var order = await _context.Orders
                .Include(x => x.Cart)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (order == null) return null;
            if (!string.IsNullOrEmpty(form.Status))
            {
                order.Status = form.Status;
                order.Updated = DateTime.Now;
            }
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new OrderForAdmin(order);
        }
       
        #endregion


        #region Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null) return false;
            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();
            return true;
        }
        
        #endregion

       

    }
}
