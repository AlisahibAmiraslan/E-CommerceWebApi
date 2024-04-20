using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace NextEcommerceWebApi.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly DataContext _context;

        public ProductSizeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSize>> GetAllProductSizes()
        {
            var productSize = await _context.ProductSizes.ToListAsync();

            return productSize;
        }

        public async Task<ProductSize> GetSingleProductSize(int id)
        {
            var productSize = await _context.ProductSizes.FindAsync(id);

            if (productSize is null)
                return null;

            return productSize;
        }

        public async Task<ActionResult<ProductSize>> AddProductSize(ProductSizeCreateDto size)
        {
            var newProductSize = new ProductSize
            {
                Color = size.Color,
                Size1Name = size.Size1Name,
                Size2Name = size.Size2Name,
                StockQty = size.StockQty,
                ProductSizeActive = size.ProductSizeActive,
                ProductId = size.ProductId
            };

            _context.ProductSizes.Add(newProductSize);

            await _context.SaveChangesAsync();

            return newProductSize;
        }

        public async Task<ActionResult<ProductSize>> UpdateProductSize(ProductSizeCreateDto request)
        {
            var size = await _context.ProductSizes.FindAsync(request.Id);

            if (size is null)
                return null;

            size.Color = request.Color;
            size.Size1Name = request.Size1Name;
            size.Size2Name = request.Size2Name;
            size.StockQty = request.StockQty;
            size.ProductSizeActive = request.ProductSizeActive;
            size.ProductId = request.ProductId;

            await _context.SaveChangesAsync();

            return size;
        }

        public async Task<ActionResult<ProductSize>> DeleteProductSize(int id)
        {
            var size = await _context.ProductSizes.FindAsync(id);

            if (size is null)
                return null;

            _context.ProductSizes.Remove(size);
            await _context.SaveChangesAsync();

            return size;
        }
    }
}
