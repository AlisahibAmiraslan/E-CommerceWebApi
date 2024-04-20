using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly DataContext _context;

        public CategoryController(ICategoryService categoryService, DataContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }

        // get all category
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories() 
        {
            try
            {
                return await _categoryService.GetAllCategories();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single category
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetSingleCategory(int id)
        {
            try
            {
                var result = await _categoryService.GetSingleCategory(id);

                if (result is null)
                    return NotFound("Category is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add category
        [HttpPost]
        public async Task<ActionResult<List<Category>>> AddCategory(Category category)
        {
            try
            {
                var result = await _categoryService.AddCategory(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update category
        [HttpPut]
        public async Task<ActionResult<List<Category>>> UpdateCategory(Category request)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(request);

                if (result is null)
                    return NotFound("Category is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete category
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Category>>> DeleteCategory(int id)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(id);

                if (result is null)
                    return NotFound("Category is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get category products
        [HttpGet("categoryproducts")]
        public async Task<ActionResult<List<Category>>> GetCategoryProducts(int id)
        {
            try
            {
                var categoryProducts = await _context.Products.Where(u=>u.CategoryId == id).ToListAsync();

                return Ok(categoryProducts);    
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get category products with all data
        [HttpGet("search")]
        public async Task<ActionResult<List<Category>>> GetCategorySearch(int id)
        {
            try
            {
                var categoryProducts = await _context.Products.Where(u => u.CategoryId == id).Include(ps => ps.ProductSizes).Include(pi => pi.ProductImages).ToListAsync();

                return Ok(categoryProducts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
