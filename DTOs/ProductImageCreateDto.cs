namespace NextEcommerceWebApi.DTOs
{
    public class ProductImageCreateDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ImagesName { get; set; }
        public string? Path { get; set; }
        public string? Url { get; set; }
    }
}
