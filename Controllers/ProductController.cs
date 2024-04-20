using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Services;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // get all product
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            try
            {
                return await _productService.GetAllProducts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single product
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetSingleProduct(int id)
        {
            try
            {
                var result = await _productService.GetSingleProduct(id);

                if (result is null)
                    return NotFound("Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add product
        [HttpPost]
        public async Task<ActionResult<List<Product>>> AddProduct(ProductCreateDto product)
        {
            try
            {
                var result = await _productService.AddProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // update product
        [HttpPut]
        public async Task<ActionResult<List<Product>>> UpdateProduct(ProductCreateDto request)
        {
            try
            {
                var result = await _productService.UpdateProduct(request);

                if (result is null)
                    return NotFound("Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete product
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            try
            {
                var result = await _productService.DeleteProduct(id);

                if (result is null)
                    return NotFound("Product is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //search products
        [HttpGet("search-products")]
        public async Task<ActionResult<List<Product>>> GetSearchProducts(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return BadRequest("The text field is required.");

                return await _productService.SearchProduct(text);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
