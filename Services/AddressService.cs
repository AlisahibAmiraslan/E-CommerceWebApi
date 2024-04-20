using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Services
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;

        public AddressService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Address>> GetAllAddresses()
        {
            var addresses = await _context.Addresses.ToListAsync();

            return addresses;
        }

        public async Task<List<Address>> GetAdressByUser(int userId)
        {
          var userAddress = await _context.Addresses.Where(u=>u.UserId == userId).ToListAsync();

          return userAddress;
        }

        public async Task<Address> GetSingleAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address is null)
                return null;

            return address;
        }

        public async Task<ActionResult<Address>> AddAddress(Address address)
        {
            var newAddress = new Address
            {
                UserId = address.UserId,
                Name = address.Name,
                SurName = address.SurName,
                Email = address.Email,
                Phone = address.Phone,
                Country = address.Country,
                City = address.City,
                DistrictName = address.DistrictName,
                AdressDescription = address.AdressDescription,
            };

            _context.Addresses.Add(newAddress);

            _context.SaveChanges();

            return newAddress;
        }

        public async Task<ActionResult<Address>> UpdateAddress(Address request)
        {
            var address = await _context.Addresses.FindAsync(request.Id);

            if (address is null)
                return null;

            address.UserId = request.UserId;
            address.Name = request.Name;
            address.SurName = request.SurName;
            address.Email = request.Email;
            address.Phone = request.Phone;
            address.Country = request.Country;
            address.City = request.City;
            address.DistrictName = request.DistrictName;
            address.AdressDescription = request.AdressDescription;

            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address is null)
                return null;

            _context.Addresses.Remove(address);
            _context.SaveChanges();

            return address;
        }
    }
}
