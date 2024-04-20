using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using System;

namespace NextEcommerceWebApi.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly DataContext _context;

        public ProductImageService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductImage>> GetAllProductImages()
        {
            var productImage = await _context.ProductImages.ToListAsync();

            return productImage;
        }

        public async Task<ProductImage> GetSingleProductImage(int id)
        {
            var productImage = await _context.ProductImages.FindAsync(id);

            if (productImage is null)
                return null;

            return productImage;
        }

        public async Task<ActionResult<ProductImage>> AddProductImage(ProductImageCreateDto image)
        {
            var newProductImage = new ProductImage
            {
                ProductId = image.ProductId,
                ImagesName = image.ImagesName,
                Path = image.Path,
                Url = image.Url
            };

            _context.ProductImages.Add(newProductImage);

            await _context.SaveChangesAsync();

            return newProductImage;
        }

        public async Task<ActionResult<ProductImage>> UpdateProductImage(ProductImageCreateDto request)
        {
            var image = await _context.ProductImages.FindAsync(request.Id);

            if (image is null)
                return null;

            image.ProductId = request.ProductId;
            image.ImagesName = request.ImagesName;
            image.Path = request.Path;
            image.Url = request.Url;

            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            var image = await _context.ProductImages.FindAsync(id);

            if (image is null)
                return null;

            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
