// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Models;

namespace WebAppMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet untuk setiap model (table)
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        // Seed data (data awal)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 20 },
                new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Age = 22 },
                new Student { Id = 3, Name = "Charlie", Email = "charlie@example.com", Age = 21 }
            );
        }
    }
}