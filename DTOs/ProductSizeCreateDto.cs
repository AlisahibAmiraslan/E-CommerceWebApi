namespace NextEcommerceWebApi.DTOs
{
    public class ProductSizeCreateDto
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Size1Name { get; set; }
        public string? Size2Name { get; set; }
        public int? StockQty { get; set; }
        public bool ProductSizeActive { get; set; } = true;
        public int ProductId { get; set; }
    }
}
