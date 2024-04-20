﻿namespace NextEcommerceWebApi.DTOs
{
    public class OrderUpdateCreateDto
    {
        public int Id { get; set; }
        public string? OrderCode { get; set; }
        public string OrderStatus { get; set; } = "Order Pending";
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardMonth { get; set; }
        public int CardYear { get; set; }
        public int CardCVC { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
