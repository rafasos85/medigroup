using Medigroup.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Medigroup.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Medicamento> Medicamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Medicamento>().ToTable("Medicamentos");
        }
    }
}
