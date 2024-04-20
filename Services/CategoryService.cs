using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
                var categories = await _context.Categories.ToListAsync();

                return categories;
        }

        public async Task<Category> GetSingleCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return null;

            return category;
        }

        public async Task<ActionResult<Category>> AddCategory(Category category)
        {
           _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<ActionResult<Category>> UpdateCategory(Category request)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category is null)
                return null;

            category.CatName = request.CatName;
            category.CatDescription = request.CatDescription;
            category.CatImg = request.CatImg;

            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return null;

            _context.Categories.Remove(category);   
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
