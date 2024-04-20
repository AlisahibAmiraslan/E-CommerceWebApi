using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IProductImageService
    {
        Task<List<ProductImage>> GetAllProductImages();
        Task<ProductImage> GetSingleProductImage(int id);
        Task<ActionResult<ProductImage>> AddProductImage(ProductImageCreateDto image);
        Task<ActionResult<ProductImage>> UpdateProductImage(ProductImageCreateDto image);
        Task<ActionResult<ProductImage>> DeleteProductImage(int id);
    }
}
