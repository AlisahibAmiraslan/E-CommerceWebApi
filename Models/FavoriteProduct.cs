namespace NextEcommerceWebApi.Models
{
    public class FavoriteProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public string ProductImage { get; set; }

    }
}
