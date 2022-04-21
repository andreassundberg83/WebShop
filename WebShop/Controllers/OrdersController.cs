using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Filters;
using WebShopAPI.Models;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;
using WebShopAPI.Services;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateOrder(PlaceOrderForm form)
        {
            var order = await _orderService.CreateAsync(form);
            return (order == null) ? BadRequest() : Ok(order);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IEnumerable<OrderForAdmin>> GetOrders()
        {
            return await _orderService.GetAllAsync();
        }

        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            if (!await _userService.CheckIfUserExistsAsync(userId)) return BadRequest($"No user with id {userId}");
            var orders = await _orderService.GetAllOrdersByUserIdAsync(userId);
            return orders == null ? BadRequest($"User with id {userId} have no placed orders") : Ok(orders);
        }

        [HttpGet("admin/{orderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetOrderForAdmin(int orderId)
        {
            var order = await _orderService.AdminGetByIdAsync(orderId);
            return order == null ? BadRequest("No such order") : Ok(order);
        }

        [HttpGet("user/{orderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetOrderForUser(int orderId)
        {            
            var order = await _orderService.UserGetByIdAsync(orderId);
            return order == null ? BadRequest("No such order") : Ok(order);
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderForm form)
        {
            var order = await _orderService.UpdateAsync(id, form);
            return order == null ? BadRequest("No such order") : Ok(order);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            return (await _orderService.DeleteAsync(id)) ?
                 Ok($"Order with id {id} successfully removed")
                :BadRequest($"An order with id {id} was not found");
        }

        
    }
}
