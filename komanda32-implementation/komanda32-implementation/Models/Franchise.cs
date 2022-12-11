namespace komanda32_implementation.Models
{
    public record Franchise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<Group> Groups { get; set; }
    }
}
