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
    public class ProductSizeController : ControllerBase
    {
        private readonly IProductSizeService _productSizeService;

        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }


        // get all product size
        [HttpGet]
        public async Task<ActionResult<List<ProductSize>>> GetAllProductSizes()
        {
            try
            {
                return await _productSizeService.GetAllProductSizes();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single product size
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSize>> GetSingleProductSize(int id)
        {
            try
            {
                var result = await _productSizeService.GetSingleProductSize(id);

                if (result is null)
                    return NotFound("Product Size is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // add product size
        [HttpPost]
        public async Task<ActionResult<List<ProductSize>>> AddProductSize(ProductSizeCreateDto size)
        {
            try
            {
                var result = await _productSizeService.AddProductSize(size);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update product size
        [HttpPut]
        public async Task<ActionResult<List<ProductSize>>> UpdateProductSize(ProductSizeCreateDto request)
        {
            try
            {
                var result = await _productSizeService.UpdateProductSize(request);

                if (result is null)
                    return NotFound("Product size is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete product size
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductSize>>> DeleteProductSize(int id)
        {
            try
            {
                var result = await _productSizeService.DeleteProductSize(id);

                if (result is null)
                    return NotFound("Product size is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
