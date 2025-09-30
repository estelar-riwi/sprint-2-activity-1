using Microsoft.EntityFrameworkCore;
using sprint_2_actividad_1.Models;

namespace sprint_2_actividad_1.Data
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "server=168.119.183.3;port=3307;user=root;password=g0tIFJEQsKHm5$34Pxu1;database=Vanessa_Gomez_Lopez";

        public DbSet<Client> Clients { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<Attention> Attentions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}