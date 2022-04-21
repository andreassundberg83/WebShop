using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShopAPI.Filters;
using WebShopAPI.Models;
using WebShopAPI.Models.Data;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;
using WebShopAPI.Services;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;

        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {           
             return await _userService.CreateAsync(form);
        }

        [HttpPost("SignIn")]

        public async Task<IActionResult> SignIn(SignInForm form)
        {
            return await _userService.SignIn(form);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? BadRequest($"No user with id {id}") : Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateUser(int id, UserUpdateForm form)
        {
            var user = await _userService.UpdateAsync(id, form);
            return user == null? BadRequest() : Ok(user);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return await _userService.DeleteAsync(id) ? 
                Ok($"User with id {id} successfully removed")
                : BadRequest($"No user with id {id}");
        }
    }
}
