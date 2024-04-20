using Microsoft.AspNetCore.Http;
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
    public class AddToCartController : ControllerBase
    {
        private readonly IAddToCartService _addToCartService;
        private readonly DataContext _context;

        public AddToCartController(IAddToCartService addToCartService, DataContext context)
        {
            _addToCartService = addToCartService;
            _context = context;
        }

        // get all carts
        [HttpGet]
        public async Task<ActionResult<List<AddToCart>>> GetAllAddToCarts()
        {
            try
            {
                return await _addToCartService.GetAllAddToCarts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single cart
        [HttpGet("{id}")]
        public async Task<ActionResult<AddToCart>> GetSingleAddToCart(int id)
        {
            try
            {
                var result = await _addToCartService.GetSingleAddToCart(id);

                if (result is null)
                    return NotFound("Cart is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add cart
        [HttpPost]
        public async Task<ActionResult<List<AddToCart>>> AddCartItem(AddToCart cart)
        {
            try
            {
                var result = await _addToCartService.AddCartItem(cart);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // update cart
        [HttpPut]
        public async Task<ActionResult<List<AddToCart>>> UpdateAddToCart(AddToCart request)
        {
            try
            {
                var result = await _addToCartService.UpdateAddToCart(request);

                if (result is null)
                    return NotFound("Cart is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // update unique cart
        [HttpPut("update-unique-cart")]
        public async Task<ActionResult<List<AddToCart>>> UpdateUniqueCart(UpdateUniqueAddToCartCreateDto request)
        {
            try
            {
                var result = await _addToCartService.UpdateUniqueAddToCart(request);

                if (result is null)
                    return NotFound("Cart is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete cart
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AddToCart>>> DeleteAddToCart(int id)
        {
            try
            {
                var result = await _addToCartService.DeleteAddToCart(id);

                if (result is null)
                    return NotFound("Cart is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("uniqueId")]
        public async Task<ActionResult<List<AddToCart>>> GetCartItems(string uniqueId)
        {
            try
            {
                var totalPrice = await _context.AddToCarts.Where(item => item.UniqueId == uniqueId).SumAsync(detail => detail.SeriePrice);

                var getCartByUniqueId = await _addToCartService.GetCartItems(uniqueId);

                var objectJson = new { getCartByUniqueId, totalPrice };

                return Ok(objectJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getCartByUser")]
        public async Task<ActionResult<List<AddToCart>>> GetCartByUser(int userId)
        {
            try
            {
                var totalPrice = await _context.AddToCarts.Where(u => u.UserId == userId).SumAsync(detail => detail.SeriePrice);

                var getCartByUser = await _addToCartService.GetCartByUser(userId);

                var objectJson = new { getCartByUser, totalPrice };

                return Ok(objectJson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
