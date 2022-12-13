namespace komanda32_implementation.Models.Create
{
    public record CreateCustomer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int FranchiseId { get; set; }
        public string Email { get; set; }
    }
}
