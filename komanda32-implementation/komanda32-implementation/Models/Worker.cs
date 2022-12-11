using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Models
{
    public record Worker
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public Franchise Franchise { get; set; }
        public Group Group { get; set; }
        public int? ReadAccess { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal Emplyment { get; set; }
        public string BankAccount { get; set; }
        public decimal? Rating { get; set; }
        public WorkerType Type { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public static void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasOne(worker => worker.Group).WithMany(group => group.Workers);
        }
    }
}
