using Microsoft.EntityFrameworkCore;
using WebShopAPI.Models;
using WebShopAPI.Models.Data;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Services
{
    public interface IProductService
    {
        public Task<Product> CreateAsync(ProductForm form);
        public Task<Product> GetByIdAsync(int id);
        public Task<IEnumerable<Product>> GetAllAsync();
        public Task<IEnumerable<Product>> GetAllFromCategoryAsync(int categoryId);
        public Task<Product> UpdateAsync(int id, ProductForm form);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> CheckIfExists(string name);
        

    }
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(ProductForm form)
        {
            if (!await CheckIfExists(form.Name))
            {
                if (!await _context.Categories.AnyAsync(x => x.Name == form.CategoryName))
                {
                    _context.Categories.Add(new CategoryEntity(form.CategoryName)); 
                    await _context.SaveChangesAsync();
                }
                var entity = new ProductEntity(form);
                var categoryEntity = await _context.Categories.FirstAsync(x => x.Name == form.CategoryName);
                entity.CategoryId = categoryEntity.Id;
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();  
                return new Product
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CategoryName = categoryEntity.Name,
                    Description = entity.Description,
                    Price = entity.Price
                };
            }
            return null!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null) return false!;
            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = new List<Product>();
            var productEntities = await _context.Products               
                .Include(c => c.Category)
                .ToListAsync();
            productEntities.ForEach(x =>
            {
                var product = new Product(x);
                product.CategoryName = x.Category.Name;
                products.Add(product);
            });
            return products;
        }
        public async Task<IEnumerable<Product>> GetAllFromCategoryAsync(int categoryId)
        {

            var products = new List<Product>();
            var productEntities = await _context.Products
                .Where(x => x.CategoryId == categoryId)
                .Include(c => c.Category)
                .ToListAsync();
            productEntities.ForEach(x =>
            {
                var product = new Product(x);
                product.CategoryName = x.Category.Name;
                products.Add(product);
            });
            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var productEntity = await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (productEntity == null)
                return null!;
            return new Product(productEntity); ;            
        }

        public async Task<Product> UpdateAsync(int id, ProductForm form)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null) 
                return null;
            if (!string.IsNullOrEmpty(form.CategoryName))
            {
                if (!await _context.Categories.AnyAsync(x => x.Name == form.CategoryName))
                {
                    _context.Categories.Add(new CategoryEntity(form.CategoryName));
                    await _context.SaveChangesAsync();                    
                }
                var categoryEntity = await _context.Categories.FirstAsync(x => x.Name == form.CategoryName);
                productEntity.CategoryId = categoryEntity.Id;

            }            
            if (!string.IsNullOrEmpty(form.Name)) productEntity.Name = form.Name;
            if (!string.IsNullOrEmpty(form.Description)) productEntity.Description = form.Description;
            if (form.Price != 0) productEntity.Price = form.Price;
            _context.Entry(productEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Product(productEntity);
        }
        public async Task<bool> CheckIfExists(string name)
        {
            return await _context.Products.AnyAsync(x => x.Name == name);
        }
    }
}
