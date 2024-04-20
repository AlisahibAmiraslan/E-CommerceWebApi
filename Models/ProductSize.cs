using System.Text.Json.Serialization;

namespace NextEcommerceWebApi.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string? Color { get; set; }
        public string? Size1Name { get; set; }
        public string? Size2Name { get; set; }
        public int? StockQty { get; set; }
        public bool ProductSizeActive { get; set; } = true;
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
