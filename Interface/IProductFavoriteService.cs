using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IProductFavoriteService
    {
        Task<List<FavoriteProduct>> GetAllFavoriteProducts();
        Task<FavoriteProduct> GetSingleFavoriteProduct(int id);
        Task<ActionResult<FavoriteProduct>> AddFavoriteProduct(FavoriteProduct product);
        Task<ActionResult<FavoriteProduct>> UpdateFavoriteProduct(FavoriteProduct product);
        Task<ActionResult<FavoriteProduct>> DeleteFavoriteProduct(int id);
        Task<List<FavoriteProduct>> GetFavoriteProductByUserId(int id);
    }
}
