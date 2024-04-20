using System.Text.Json.Serialization;

namespace NextEcommerceWebApi.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public string? ImagesName { get; set; }
        public string? Path { get; set; }
        public string? Url { get; set; }

    }
}
