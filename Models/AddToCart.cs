namespace NextEcommerceWebApi.Models
{
    public class AddToCart
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 0;
        public string UniqueId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? MarkName { get; set; }
        public int ProductPrice { get; set; }
        public int Qty { get; set; } = 1;
        public int ProductSizeId { get; set; }
        public string? ProductColor { get; set; }
        public string? ProductSize1Name { get; set; }
        public string? ProductSize2Name { get; set; }
        public int SeriePrice { get; set; }
        public string? ProductImage { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
