using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebShopAPI.Filters;
using WebShopAPI.Models;
using WebShopAPI.Models.Forms;
using WebShopAPI.Services;

namespace WebShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AdminApiKey]
    
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryForm form)
        {
            var category = await _categoryService.CreateAsync(form);
            return (category == null) ? new BadRequestResult() : new OkObjectResult(category);
        }
       
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _categoryService.GetAllAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return (category == null) ? NotFound($"Category with id {id} not found") : Ok(category);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, CategoryForm form)
        {
            var category = await _categoryService.UpdateAsync(id, form);
            return (category == null) ? NotFound($"Category with id {id} not found") : Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var productsInCatecory = await _productService.GetAllFromCategoryAsync(id);
            if (productsInCatecory.Any())
            {
                return BadRequest("Not allowed to remove Category containing Products");
            }
            var category = await _categoryService.DeleteAsync(id);
            return Ok($"{category.Name} with id {category.Id} successfully removed");
        }
    }
}
