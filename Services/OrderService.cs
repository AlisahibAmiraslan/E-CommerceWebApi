using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Migrations;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        public OrderService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var order = await _context.Orders.Include(o=>o.OrderDetails).Include(o=>o.OrderAddress).AsSplitQuery().ToListAsync();

            return order;
        }

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
           var userOrders = await _context.Orders.Where(item=>item.UserId == userId).Include(o=>o.OrderDetails).Include(o=>o.OrderAddress).AsSplitQuery().ToListAsync();
            
           return userOrders;
        }

        public async Task<Order> GetSingleOrder(int id)
        {
            var order = await _context.Orders.Include(od => od.OrderDetails).Include(a=>a.OrderAddress).AsSplitQuery().FirstOrDefaultAsync(o=>o.Id == id);

            if (order is null)
                return null;

            return order;
        }

        public async Task<ActionResult<List<Order>>> AddOrders(OrderCreateDto orders)
        {
            var newOrder = new Order()
            {
                OrderStatus = orders.OrderStatus,
                OrderCode = GenerateRandomOrderCode(),
                UserId = orders.UserId,
                CardNumber = orders.CardNumber,
                CardName = orders.CardName,
                CardMonth = orders.CardMonth,
                CardYear = orders.CardYear,
                CardCVC = orders.CardCVC,
                OrderDate = orders.OrderDate,
            };

            var orderDetail = orders.OrderDetails.Select(o=>new OrderDetails
            {
                UserId = o.UserId,
                UniqueId = o.UniqueId,
                ProductId = o.ProductId,
                ProductName = o.ProductName,
                ProductDescription = o.ProductDescription,
                MarkName = o.MarkName,
                ProductPrice = o.ProductPrice,
                Qty = o.Qty,
                ProductSizeId = o.ProductSizeId,
                ProductColor = o.ProductColor,
                ProductSize1Name = o.ProductSize1Name,
                ProductSize2Name = o.ProductSize2Name,
                SeriePrice = o.SeriePrice,
                ProductImage = o.ProductImage,
                Created = o.Created,
                Order = newOrder
            }).ToList();

            newOrder.OrderDetails = orderDetail;

            var orderAddress = orders.OrderAddresses.Select(a => new OrderAddress
            {
                UserId = a.UserId,
                Name = a.Name,
                SurName = a.SurName,
                Email = a.Email,
                Phone = a.Phone,
                Country = a.Country,
                City = a.City,
                DistrictName = a.DistrictName,
                AdressDescription = a.AdressDescription,
            }).ToList();

            newOrder.OrderAddress = orderAddress;

            _context.Orders.Add(newOrder);

            await _context.SaveChangesAsync();

            return await _context.Orders.Include(c => c.OrderDetails).Include(a => a.OrderAddress).AsSplitQuery().ToListAsync();

       


        }

        public async Task<ActionResult<Order>> UpdateOrder(OrderUpdateCreateDto request)
        {
            var order = await _context.Orders.FindAsync(request.Id);

            if (order is null)
                return null;

                order.OrderCode = request.OrderCode;
                order.OrderStatus = request.OrderStatus;
                order.UserId = request.UserId;
                order.CardNumber = request.CardNumber;
                order.CardName = request.CardName;
                order.CardMonth = request.CardMonth;
                order.CardYear = request.CardYear;
                order.CardCVC = request.CardCVC;
                order.OrderDate = request.OrderDate;

            await _context.SaveChangesAsync();

            return order;

        }

        public async Task<ActionResult<OrderDetails>> UpdateOrderDetails(OrderDetailsCreateDto request)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(request.Id);

            if (orderDetail is null)
                return null;

            orderDetail.UserId = request.UserId;
            orderDetail.UniqueId = request.UniqueId;
            orderDetail.ProductId = request.ProductId;
            orderDetail.ProductName = request.ProductName;
            orderDetail.ProductDescription = request.ProductDescription;
            orderDetail.MarkName = request.MarkName;
            orderDetail.ProductPrice = request.ProductPrice;
            orderDetail.Qty = request.Qty;
            orderDetail.ProductSizeId = request.ProductSizeId;
            orderDetail.ProductSize1Name = request.ProductSize1Name;
            orderDetail.ProductSize2Name = request.ProductSize2Name;
            orderDetail.SeriePrice = request.SeriePrice;
            orderDetail.ProductImage = request.ProductImage;
            orderDetail.Created = request.Created;

            await _context.SaveChangesAsync();

            return orderDetail;
        }

        public async Task<ActionResult<OrderAddress>> UpdateOrderAddress(OrderAddressCreateDto address)
        {
            var orderAddress = await _context.OrderAddresses.FindAsync(address.Id);

            if (orderAddress is null)
                return null;

            orderAddress.UserId = address.UserId;
            orderAddress.Name = address.Name;
            orderAddress.SurName = address.SurName;
            orderAddress.Email = address.Email;
            orderAddress.Phone = address.Phone;
            orderAddress.Country = address.Country;
            orderAddress.City = address.City;
            orderAddress.DistrictName = address.DistrictName;
            orderAddress.AdressDescription = address.AdressDescription;

            await _context.SaveChangesAsync();

            return orderAddress;
        }

        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order is null)
                return null;

            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<AddToCart>> DeleteOrderByCart(string uniqueId)
        {
            var findCartByUniqueId = await _context.AddToCarts.Where(u => u.UniqueId == uniqueId).ToListAsync();

            if (findCartByUniqueId is null)
                return null;

            _context.AddToCarts.RemoveRange(findCartByUniqueId);

            await _context.SaveChangesAsync();

            return findCartByUniqueId;
        }

        private string GenerateRandomOrderCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new char[6];
            for (int i = 0; i < 6; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }
    }
}