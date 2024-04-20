using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NextEcommerceWebApi.Data;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Interface;
using NextEcommerceWebApi.Models;
using NextEcommerceWebApi.Services;

namespace NextEcommerceWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly DataContext _context;
        public OrderController(IOrderService orderService, DataContext context)
        {
            _orderService = orderService;
            _context = context;
        }

        // get all orders
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            try
            {
                return await _orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get single order
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetSingleOrder(int id)
        {
            try
            {
                var result = await _orderService.GetSingleOrder(id);

                var totalPrice = await _context.OrderDetails.SumAsync(detail => detail.SeriePrice);

                if (result is null)
                    return NotFound("Order is not found!");

                var orderObject = new {result, totalPrice};

                return Ok(orderObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // add order
        [HttpPost]
        public async Task<ActionResult<List<Order>>> AddOrders(OrderCreateDto orders)
        {
            try
            {
                var result = await _orderService.AddOrders(orders);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // update order
        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrder(OrderUpdateCreateDto request)
        {
            try
            {
                var result = await _orderService.UpdateOrder(request);

                if (result is null)
                    return NotFound("Order is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // update order details
        [HttpPut("update-order-detail")]
        public async Task<ActionResult<OrderDetails>> UpdateOrderDetail(OrderDetailsCreateDto request)
        {
            try
            {
                var result = await _orderService.UpdateOrderDetails(request);

                if (result is null)
                    return NotFound("Order Detail is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // update order adress
        [HttpPut("update-order-address")]
        public async Task<ActionResult<OrderAddress>> UpdateAddress(OrderAddressCreateDto request)
        {
            try
            {
                var result = await _orderService.UpdateOrderAddress(request);

                if (result is null)
                    return NotFound("Order Address is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete order
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Order>>> DeleteOrder(int id)
        {
            try
            {
                var result = await _orderService.DeleteOrder(id);

                if (result is null)
                    return NotFound("Order is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //delete order by unique Id
        [HttpDelete("deleteByUniqueId")]
        public async Task<ActionResult<List<Order>>> DeleteOrderByUniqueId(string id)
        {
            try
            {
                var result = await _orderService.DeleteOrderByCart(id);

                if (result is null)
                    return NotFound("Cart is not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get order details
        [HttpGet("orderdetails-with-orderId")]
        public async Task<ActionResult<List<OrderDetails>>> GetOrderDetails(int id)
        {
            try
            {
                var orderDetails = await _context.OrderDetails.Where(u => u.OrderId == id).ToListAsync();

                var totalPrice = await _context.OrderDetails.Where(u=>u.OrderId == id).SumAsync(detail => detail.SeriePrice);

                var orderObject = new {orderDetails, totalPrice};

                return Ok(orderObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get order adresses
        [HttpGet("orderderaddress-with-orderId")]
        public async Task<ActionResult<List<OrderAddress>>> GetOrderAddresses(int id)
        {
            try
            {
                var orderAddresses = await _context.OrderAddresses.Where(u => u.OrderId == id).ToListAsync();

                return Ok(orderAddresses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get order by user
        [HttpGet("getOrderByUser")]
        public async Task<ActionResult<List<Order>>> GetOrdersByUser(int userid)
        {
            try
            {
                var ordersByUser = await _orderService.GetOrdersByUser(userid);

                int totalPriceDetails;

                foreach (var order in ordersByUser)
                {
                    totalPriceDetails = order.OrderDetails.Sum(detail => detail.SeriePrice);
                    
                }

                var totalPriceAllOrders = await _context.OrderDetails.Where(u=>u.UserId==userid).SumAsync(detail => detail.SeriePrice);

                var response = new
                {
                    ordersByUser = ordersByUser.Select(o => new
                    {
                        o.Id,
                        o.OrderCode,
                        o.OrderStatus,
                        o.UserId,
                        o.CardNumber,
                        o.CardName,
                        o.CardMonth,
                        o.CardYear,
                        o.CardCVC,
                        o.OrderDate,
                        //OrderDetails = o.OrderDetails,
                        o.OrderDetails,
                        o.OrderAddress,
                        TotalPrice = o.OrderDetails.Sum(d => d.SeriePrice)
                    }),
                    totalPriceAllOrders
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
