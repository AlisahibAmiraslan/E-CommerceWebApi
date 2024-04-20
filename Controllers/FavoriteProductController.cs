using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Services;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FavoriteProductController : ControllerBase
    {
        private readonly IProductFavoriteService _favoriteService;
        private readonly DataContext _context;

        public FavoriteProductController(IProductFavoriteService favoriteService, DataContext context)
        {
            _favoriteService = favoriteService;
            _context = context;
        }

        // get all favorite product 
        [HttpGet,AllowAnonymous]
        public async Task<ActionResult<List<FavoriteProduct>>> GetAllFavoriteProducts()
        {
            try
            {
                return await _favoriteService.GetAllFavoriteProducts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single favorite product
        [HttpGet("{id}"),AllowAnonymous]
        public async Task<ActionResult<FavoriteProduct>> GetSingleFavoriteProduct(int id)
        {
            try
            {
                var result = await _favoriteService.GetSingleFavoriteProduct(id);

                if (result is null)
                    return NotFound("Favorite Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // add favorite product
        [HttpPost]
        public async Task<ActionResult<List<FavoriteProduct>>> AddFavoriteProduct(FavoriteProduct product)
        {
            try
            {
                var result = await _favoriteService.AddFavoriteProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update favorite product
        [HttpPut]
        public async Task<ActionResult<List<FavoriteProduct>>> UpdateFavoriteProduct(FavoriteProduct request)
        {
            try
            {
                var result = await _favoriteService.UpdateFavoriteProduct(request);

                if (result is null)
                    return NotFound("Favorite Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete product favorite
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<FavoriteProduct>>> DeleteFavoriteProduct(int id)
        {
            try
            {
                var result = await _favoriteService.DeleteFavoriteProduct(id);

                if (result is null)
                    return NotFound("Favorite Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get fav for user id
        [HttpGet("getFavByUser"),AllowAnonymous]
        public async Task<ActionResult<List<FavoriteProduct>>>GetFavProductByUser(int id)
        {
            try
            {
               return await _favoriteService.GetFavoriteProductByUserId(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
