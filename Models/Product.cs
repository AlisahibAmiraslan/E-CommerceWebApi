namespace NextEcommerceWebApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductDetail { get; set; }
        public string ProductCatImage { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set;}
        public string CategoryName { get; set; }
        public string? MarkName { get; set; }
        public string? Season { get; set; } 
        public int StockQty { get; set; } = 0;
        public DateTime Created { get; set; } = DateTime.Now;
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }

}
