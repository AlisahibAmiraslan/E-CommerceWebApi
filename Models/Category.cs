namespace NextEcommerceWebApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public string? CatImg { get; set; } = string.Empty;
        public string? CatDescription { get; set; }
    }
}
