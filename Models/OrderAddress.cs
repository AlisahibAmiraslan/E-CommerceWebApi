using System.Text.Json.Serialization;

namespace NextEcommerceWebApi.Models
{
    public class OrderAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; } = 0;
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string DistrictName { get; set; }
        public string AdressDescription { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
