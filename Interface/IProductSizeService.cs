using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IProductSizeService
    {
        Task<List<ProductSize>> GetAllProductSizes();
        Task<ProductSize> GetSingleProductSize(int id);
        Task<ActionResult<ProductSize>> AddProductSize(ProductSizeCreateDto image);
        Task<ActionResult<ProductSize>> UpdateProductSize(ProductSizeCreateDto image);
        Task<ActionResult<ProductSize>> DeleteProductSize(int id);
    }
}
