using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Services;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // get all product image
        [HttpGet]
        public async Task<ActionResult<List<ProductImage>>> GetAllProductImages()
        {
            try
            {
                return await _productImageService.GetAllProductImages();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single product image
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetSingleProductImage(int id)
        {
            try
            {
                var result = await _productImageService.GetSingleProductImage(id);

                if (result is null)
                    return NotFound("Product Image is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add product image
        [HttpPost]
        public async Task<ActionResult<List<ProductImage>>> AddProductImage(ProductImageCreateDto image)
        {
            try
            {
                var result = await _productImageService.AddProductImage(image);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update product image
        [HttpPut]
        public async Task<ActionResult<List<ProductImage>>> UpdateProductImage(ProductImageCreateDto request)
        {
            try
            {
                var result = await _productImageService.UpdateProductImage(request);

                if (result is null)
                    return NotFound("Product image is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete product image
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductImage>>> DeleteProductImage(int id)
        {
            try
            {
                var result = await _productImageService.DeleteProductImage(id);

                if (result is null)
                    return NotFound("Product image is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
