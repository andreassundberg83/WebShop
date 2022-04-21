using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShopAPI.Models;
using WebShopAPI.Models.Data;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Services
{
    public interface IUserService
    {
        Task<IActionResult> CreateAsync(SignUpForm form);
        Task<IActionResult> SignIn(SignInForm form);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> UpdateAsync(int id, UserUpdateForm form);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckIfUserExistsAsync(int id);
    }
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> CreateAsync(SignUpForm form)
            {
                try
                {
                    if (await _context.Users.AnyAsync(x => x.Email == form.Email))
                        return new ConflictObjectResult("A user with that email already exists");

                    var userEntity = new UserEntity(form);
                    _context.Users.Add(userEntity);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult("User created successfully");
                }
                catch
                {
                    return new BadRequestResult();
                }
            }

        public async Task<bool> DeleteAsync(int id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null) return false;    
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            (await _context.Users.ToListAsync()).ForEach(x => users.Add(new User(x)));
            return users;
        }


        public async Task<User> GetByIdAsync(int id)
        {
            if (!await CheckIfUserExistsAsync(id)) return null!;
            return new User(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (string.IsNullOrEmpty(form.Email) || string.IsNullOrEmpty(form.Password))
                return new BadRequestObjectResult("Email and password must be provided");

            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == form.Email);
            if (userEntity == null)
                return new BadRequestObjectResult("Incorrect email or password");

            if (!userEntity.CompareSecurePassword(form.Password))
                return new BadRequestObjectResult("Incorrect email or password");

            
            string token = CreateToken(userEntity);
            var loginConfirmation = new LoginConfirmation
            {
                UserId = userEntity.Id,
                Jwt = token,
                IsAdmin = userEntity.IsAdmin
            };
            return new OkObjectResult(loginConfirmation);
        }

        private string CreateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin? "Admin" : "User")
                
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
                
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            
            return jwt;
        }

        public async Task<User> UpdateAsync(int id, UserUpdateForm form)
        {
            var userEntity = await _context.Users.FindAsync(id);
            if (userEntity == null) return null!;
            if (!string.IsNullOrEmpty(form.FirstName)) userEntity.FirstName = form.FirstName;
            if (!string.IsNullOrEmpty(form.LastName)) userEntity.LastName = form.LastName;
            if (!string.IsNullOrEmpty(form.Password)) userEntity.CreateSecurePassword(form.Password);
            if (!string.IsNullOrEmpty(form.Address)) userEntity.Address = form.Address;
            if (!string.IsNullOrEmpty(form.City)) userEntity.City = form.City;
            if (!string.IsNullOrEmpty(form.PostalCode)) userEntity.PostalCode = form.PostalCode;
            userEntity.IsAdmin = form.IsAdmin;
            _context.Entry(userEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new User(userEntity);
            
        }
        public async Task<bool> CheckIfUserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(x => x.Id == id);
        }
    }
}
