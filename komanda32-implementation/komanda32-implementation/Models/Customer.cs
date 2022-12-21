using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Models
{
    public record Customer : IAuthenticatable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Franchise Franchise { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? DeliveryAddress { get; set; }
        public bool Blocked { get; set; }

        public Customer(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            Name = Name;
            Blocked = false;
        }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasOne(customer => customer.Franchise).WithMany(franchise => franchise.Customers);
        }
    }
}
