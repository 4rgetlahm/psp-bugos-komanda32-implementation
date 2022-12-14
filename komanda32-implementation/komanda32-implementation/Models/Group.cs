using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Models
{
    public record Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Franchise Franchise { get; set; }
        public List<Worker> Workers { get; set; }
        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasOne(group => group.Franchise).WithMany(franchise => franchise.Groups);
        }
    }
}
