using System.Text.Json.Serialization;

namespace komanda32_implementation.Models
{
    public record Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public List<Group> Groups { get; set; }
        [JsonIgnore]
        public List<Customer> Customers { get; set; }
    }
}
