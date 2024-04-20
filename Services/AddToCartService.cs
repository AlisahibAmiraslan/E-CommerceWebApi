using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace NextEcommerceWebApi.Services
{
    public class AddToCartService : IAddToCartService
    {
        private readonly DataContext _context;

        public AddToCartService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<AddToCart>> GetAllAddToCarts()
        {
            var productCart = await _context.AddToCarts.ToListAsync();

            return productCart;
        }

        public async Task<AddToCart> GetSingleAddToCart(int id)
        {
            var productCart = await _context.AddToCarts.FindAsync(id);

            if (productCart is null)
                return null;

            return productCart;
        }

        public async Task<ActionResult<AddToCart>> AddCartItem(AddToCart cart)
        {
            var newProductCart = new AddToCart();

            var existingItem = await _context.AddToCarts.FirstOrDefaultAsync(item => item.ProductId == cart.ProductId && item.UniqueId == cart.UniqueId && item.ProductSizeId == cart.ProductSizeId && item.ProductColor==cart.ProductColor && item.ProductSize1Name == cart.ProductSize1Name && item.ProductSize2Name == cart.ProductSize2Name);

            if (existingItem != null)
            {
                existingItem.Qty += cart.Qty;
                existingItem.SeriePrice += cart.SeriePrice;
            }
            else
            {
                 newProductCart = new AddToCart
                {
                    UserId = cart.UserId,
                    UniqueId = cart.UniqueId,
                    ProductId = cart.ProductId,
                    ProductName = cart.ProductName,
                    MarkName = cart.MarkName,
                    ProductDescription = cart.ProductDescription,
                    ProductPrice = cart.ProductPrice,
                    Qty = cart.Qty,
                    ProductSizeId = cart.ProductSizeId,
                    ProductColor = cart.ProductColor,
                    ProductSize1Name = cart.ProductSize1Name,
                    ProductSize2Name = cart.ProductSize2Name,
                    SeriePrice = cart.SeriePrice,
                    ProductImage = cart.ProductImage,
                    Created = cart.Created,
                };

                _context.AddToCarts.Add(newProductCart);
            }

            await _context.SaveChangesAsync();

            return newProductCart;
        }

        public async Task<ActionResult<AddToCart>> UpdateAddToCart(AddToCart request)
        {
            var cart = await _context.AddToCarts.FindAsync(request.Id);

            if (cart is null)
                return null;

            cart.UserId = request.UserId;
            cart.UniqueId = request.UniqueId;
            cart.ProductId = request.ProductId;
            cart.ProductName = request.ProductName;
            cart.MarkName = request.MarkName;
            cart.ProductDescription = request.ProductDescription;
            cart.ProductPrice = request.ProductPrice;
            cart.Qty = request.Qty;
            cart.ProductSizeId = request.ProductSizeId;
            cart.ProductColor = request.ProductColor;
            cart.ProductSize1Name = request.ProductSize1Name;
            cart.ProductSize2Name = request.ProductSize2Name;
            cart.SeriePrice = request.SeriePrice;
            cart.ProductImage = request.ProductImage;
            cart.Created = request.Created;

            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<ActionResult<AddToCart>> UpdateUniqueAddToCart(UpdateUniqueAddToCartCreateDto request)
        {
            var cart = await _context.AddToCarts.FindAsync(request.Id);
           
            if (cart is null)
                return null;

            cart.UserId = request.UserId;
            cart.UniqueId = request.UniqueId;
            cart.ProductId = request.ProductId;
            cart.ProductPrice = request.ProductPrice;
            cart.Qty = request.Qty;
            cart.SeriePrice = request.SeriePrice;

            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<ActionResult<AddToCart>> DeleteAddToCart(int id)
        {
            var cart = await _context.AddToCarts.FindAsync(id);

            if (cart is null)
                return null;

            _context.AddToCarts.Remove(cart);

            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task<List<AddToCart>> GetCartItems(string uniuqeId)
        {
            var uniqueCarts = await _context.AddToCarts.Where(item=>item.UniqueId == uniuqeId).ToListAsync();
            return uniqueCarts;
        }

        public async Task<List<AddToCart>> GetCartByUser(int userId)
        {
           var cartsByUser = await _context.AddToCarts.Where(user=>user.UserId == userId).ToListAsync(); 

           return cartsByUser;
        }
    }
}
