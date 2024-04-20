using Microsoft.AspNetCore.Mvc;
using NextEcommerceWebApi.DTOs;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetOrdersByUser(int userId);
        Task<Order> GetSingleOrder(int id);
        Task<ActionResult<List<Order>>> AddOrders(OrderCreateDto orders);
        Task<ActionResult<Order>> UpdateOrder(OrderUpdateCreateDto order);
        Task<ActionResult<OrderDetails>> UpdateOrderDetails(OrderDetailsCreateDto order);
        Task<ActionResult<OrderAddress>> UpdateOrderAddress(OrderAddressCreateDto address);
        Task<ActionResult<Order>> DeleteOrder(int id);
        Task<List<AddToCart>> DeleteOrderByCart(string uniqueId);
    }
}
