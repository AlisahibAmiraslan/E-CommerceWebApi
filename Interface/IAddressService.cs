using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddresses();
        Task<List<Address>> GetAdressByUser(int userId);
        Task<Address> GetSingleAddress(int id);
        Task<ActionResult<Address>> AddAddress(Address address);
        Task<ActionResult<Address>> UpdateAddress(Address address);
        Task<ActionResult<Address>> DeleteAddress(int id);
    }
}
