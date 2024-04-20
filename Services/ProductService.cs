using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.Include(pi=>pi.ProductImages).Include(ps=>ps.ProductSizes).ToListAsync();

            return products;
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            var product = await _context.Products.Include(pi => pi.ProductImages).Include(ps => ps.ProductSizes).FirstOrDefaultAsync(p=>p.Id == id);

            if (product is null)
                return null;

            return product;
        }
        public async Task <ActionResult<Product>> AddProduct(ProductCreateDto product)
        {
            var newProduct = new Product
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                ProductDetail = product.ProductDetail,
                ProductCatImage = product.ProductCatImage,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = product.CategoryName,
                MarkName = product.MarkName,
                Season = product.Season,
                StockQty = product.StockQty,
                Created = product.Created
            };


            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<ActionResult<Product>> UpdateProduct(ProductCreateDto request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            if (product is null)
                return null;

            product.ProductName = request.ProductName;
            product.ProductDescription = request.ProductDescription;
            product.ProductDetail = request.ProductDetail;
            product.ProductCatImage = request.ProductCatImage;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.CategoryName = request.CategoryName;
            product.MarkName = request.MarkName;
            product.Season = request.Season;
            product.StockQty = request.StockQty;

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null)
                return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }


        public async Task<List<Product>> SearchProduct(string text)
        {
            var products = await _context.Products.Include(s=>s.ProductSizes).Include(i=>i.ProductImages).ToListAsync();

            if (!string.IsNullOrEmpty(text))
            {
                products = products.Where(e => e.ProductName.ToLower().Contains(text.ToLower())).ToList();
            }

            return products;
        }
    }
}
