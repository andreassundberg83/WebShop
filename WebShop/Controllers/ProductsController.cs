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
    
    
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(ProductForm form)
        {
            var product = await _productService.CreateAsync(form);
            return (product == null) ? new BadRequestResult() : new OkObjectResult(product);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return (product == null) ? NotFound($"Product with id {id} not found") : Ok(product);
        }

        [HttpGet("category/{categoryId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetProductsFromCategory(int categoryId)
        {
            var products = await _productService.GetAllFromCategoryAsync(categoryId);           
            return (products == null) ? NotFound($"No products found in category with id: {categoryId}") : Ok(products);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int id, ProductForm form)
        {
            var product = await _productService.UpdateAsync(id, form);
            return (product == null) ? NotFound($"Product with id {id} not found") : Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]

        public async Task<IActionResult> DeleteProduct(int id)
        {
            return await _productService.DeleteAsync(id) ?  Ok($"Product with id {id} successfully removed") : NotFound($"No entry with id: {id}") ;
        }
        
    }
}
