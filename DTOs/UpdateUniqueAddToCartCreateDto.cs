namespace NextEcommerceWebApi.DTOs
{
    public class UpdateUniqueAddToCartCreateDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 0;
        public string UniqueId { get; set; }
        public int ProductId { get; set; }
        public int ProductPrice { get; set; }
        public int Qty { get; set; } = 1;
        public int SeriePrice { get; set; }
    }
}
