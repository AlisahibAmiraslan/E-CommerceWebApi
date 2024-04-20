using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace NextEcommerceWebApi.Services
{
    public class ProductFavoriteService : IProductFavoriteService
    {
        private readonly DataContext _context;

        public ProductFavoriteService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteProduct>> GetAllFavoriteProducts()
        {
            var productFavorite = await _context.FavoriteProducts.ToListAsync();

            return productFavorite;
        }

        public async Task<FavoriteProduct> GetSingleFavoriteProduct(int id)
        {
            var productFavorite = await _context.FavoriteProducts.FindAsync(id);

            if (productFavorite is null)
                return null;

            return productFavorite;
        }
        public async Task<ActionResult<FavoriteProduct>> AddFavoriteProduct(FavoriteProduct product)
        {
            var newProductFavorite = new FavoriteProduct
            {
                ProductId = product.ProductId,
                UserId = product.UserId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductPrice = product.ProductPrice,
                ProductImage = product.ProductImage,
            };

            _context.FavoriteProducts.Add(newProductFavorite);

            await _context.SaveChangesAsync();

            return newProductFavorite;
        }

        public async Task<ActionResult<FavoriteProduct>> UpdateFavoriteProduct(FavoriteProduct request)
        {
            var productFavorite = await _context.FavoriteProducts.FindAsync(request.Id);

            if (productFavorite is null)
                return null;

            productFavorite.ProductId = request.ProductId;
            productFavorite.UserId = request.UserId;
            productFavorite.ProductName = request.ProductName;
            productFavorite.ProductDescription = request.ProductDescription;
            productFavorite.ProductPrice = request.ProductPrice;
            productFavorite.ProductImage = request.ProductImage;

            await _context.SaveChangesAsync();

            return productFavorite;
        }

        public async Task<ActionResult<FavoriteProduct>> DeleteFavoriteProduct(int id)
        {
            var productFavorite = await _context.FavoriteProducts.FindAsync(id);

            if (productFavorite is null)
                return null;

            _context.FavoriteProducts.Remove(productFavorite);
            await _context.SaveChangesAsync();

            return productFavorite;
        }

        public async Task<List<FavoriteProduct>> GetFavoriteProductByUserId(int id)
        {
            var favProduct = await _context.FavoriteProducts.Where(u => u.UserId == id).ToListAsync();

            if (favProduct is null)
                return null;

            return favProduct;
        }
    }
}
