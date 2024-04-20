using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetSingleCategory(int id);
        Task<ActionResult<Category>> AddCategory(Category category);
        Task<ActionResult<Category>> UpdateCategory(Category category);
        Task<ActionResult<Category>> DeleteCategory(int id);
    }
}
