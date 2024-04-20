using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IAddToCartService
    {
        Task<List<AddToCart>> GetAllAddToCarts();
        Task<AddToCart> GetSingleAddToCart(int id);
        Task<List<AddToCart>> GetCartByUser(int userId);
        Task<List<AddToCart>> GetCartItems(string uniuqeId);
        Task<ActionResult<AddToCart>> AddCartItem(AddToCart cart);
        Task<ActionResult<AddToCart>> UpdateAddToCart(AddToCart cart);
        Task<ActionResult<AddToCart>> UpdateUniqueAddToCart(UpdateUniqueAddToCartCreateDto cart);
        Task<ActionResult<AddToCart>> DeleteAddToCart(int id);
    }
}
