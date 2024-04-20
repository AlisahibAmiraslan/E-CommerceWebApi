using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Services;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // get all adresses
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAllAddresses()
        {
            try
            {
                return await _addressService.GetAllAddresses();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get address via user id
        [HttpGet("getAddressByUser")]
        public async Task<ActionResult<List<Address>>> GetAddressByUser(int userId)
        {
            try
            {
                var getCartByUser = await _addressService.GetAdressByUser(userId);

                return Ok(getCartByUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single address
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetSingleAddress(int id)
        {
            try
            {
                var result = await _addressService.GetSingleAddress(id);

                if (result is null)
                    return NotFound("Address is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add address
        [HttpPost]
        public async Task<ActionResult<List<Address>>> AddAddress(Address address)
        {
            try
            {
                var result = await _addressService.AddAddress(address);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update address
        [HttpPut]
        public async Task<ActionResult<List<Address>>> UpdateAddress(Address request)
        {
            try
            {
                var result = await _addressService.UpdateAddress(request);

                if (result is null)
                    return NotFound("Address is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        //delete address
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Address>>> DeleteAddress(int id)
        {
            try
            {
                var result = await _addressService.DeleteAddress(id);

                if (result is null)
                    return NotFound("Address is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
