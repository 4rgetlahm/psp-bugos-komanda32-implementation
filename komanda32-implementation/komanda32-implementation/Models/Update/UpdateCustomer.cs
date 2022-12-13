namespace komanda32_implementation.Models.Update
{
    public record UpdateCustomer
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? FranchiseId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DeliveryAddress { get; set; }
        public bool? Blocked { get; set; }
    }
}
