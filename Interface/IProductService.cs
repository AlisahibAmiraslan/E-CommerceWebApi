using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetSingleProduct(int id);
        Task<ActionResult<Product>> AddProduct(ProductCreateDto product);
        Task<ActionResult<Product>> UpdateProduct(ProductCreateDto product);
        Task<ActionResult<Product>> DeleteProduct(int id);
        Task<List<Product>> SearchProduct(string text);
    }
}
