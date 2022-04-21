using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopAPI.Models;
using WebShopAPI.Models.Data;
using WebShopAPI.Models.Entities;
using WebShopAPI.Models.Forms;

namespace WebShopAPI.Services
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(CategoryForm form);
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> UpdateAsync(int id, CategoryForm form);
        Task<Category> DeleteAsync(int id);
        Task<CategoryEntity> GetByNameAsync(string name);
    }
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(CategoryForm form)
        {
            if (!await _context.Categories.AnyAsync(x => x.Name == form.Name))
            {
                var categoryEntity = new CategoryEntity(form.Name);
                _context.Categories.Add(categoryEntity);
                await _context.SaveChangesAsync();

                return new Category(categoryEntity.Id, categoryEntity.Name);
            }

            return null!;
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return null!;
            }
            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();
            return new Category(categoryEntity.Id, categoryEntity.Name);
           
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = new List<Category>();
            foreach (var categoryEntity in await _context.Categories.ToListAsync())
            { 
                categories.Add(new Category(categoryEntity.Id, categoryEntity.Name));
            }
            return categories;
        }
       
        public async Task<Category> GetByIdAsync(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);          
            return (categoryEntity == null) ? null! : new Category(categoryEntity.Id, categoryEntity.Name);
        }

        public async Task<Category> UpdateAsync(int id, CategoryForm form)
        {                       
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null) return null!;
            categoryEntity.Name = form.Name;
            _context.Entry(categoryEntity).State = EntityState.Modified;
            _context.SaveChanges();
            return await GetByIdAsync(id);
        }
        public async Task<CategoryEntity> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstAsync(x => x.Name == name);
        }
    }
}
