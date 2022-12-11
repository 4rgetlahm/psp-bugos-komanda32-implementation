using komanda32_implementation.Models;
using Microsoft.EntityFrameworkCore;

namespace komanda32_implementation.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Franchise> Franchises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server = Environment.GetEnvironmentVariable("SQL_SERVER") ?? "";
            string databaseName = Environment.GetEnvironmentVariable("SQL_DATABASE") ?? "";
            string user = Environment.GetEnvironmentVariable("SQL_USER") ?? "";
            string password = Environment.GetEnvironmentVariable("SQL_PASS") ?? "";

            optionsBuilder.UseMySql($"server={server};database={databaseName};user={user};password={password}", new MySqlServerVersion("8.0.31"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Worker.BuildModel(modelBuilder);
            Group.BuildModel(modelBuilder);
        }
    }
}
