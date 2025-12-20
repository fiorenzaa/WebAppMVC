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
    public DbSet<Course> Course { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    // Seed data (data awal)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Seed Students
      modelBuilder.Entity<Student>()
          .ToTable("Student")
          .HasData(
          new Student { Id = 1, Name = "Alice", Email = "alice@example.com", Age = 20 },
          new Student { Id = 2, Name = "Bob", Email = "bob@example.com", Age = 22 },
          new Student { Id = 3, Name = "Charlie", Email = "charlie@example.com", Age = 21 }
      );

      // Seed Attendance
      modelBuilder.Entity<Attendance>()
          .ToTable("Attendance")
          .HasData(
          new Attendance
          {
            Id = 1,
            Participant = "Alice",
            Date = DateTime.Today,
            Status = "Hadir",
            Notes = "On time"
          },
          new Attendance
          {
            Id = 2,
            Participant = "Bob",
            Date = DateTime.Today,
            Status = "Tidak Hadir",
            Notes = "Sakit"
          }
      );

      modelBuilder.Entity<Course>()
        .ToTable("Course")
        .HasData(
        new Course
        {
          Id = 1,
          CourseName = "Algoritma Pemrograman",
          Lecturer = "Bunyamin, S.Kom, M.Kom.",
          Day = "Monday",
          Start = "01.00 PM",
          End = "03.00 PM",
          Place = "TULT 1201"
        },
        new Course
        {
          Id = 2,
          CourseName = "Sistem Basis Data",
          Lecturer = "Veronikha, S.Kom, M.Kom.",
          Day = "Wednesday",
          Start = "08.00 AM",
          End = "10.00 AM",
          Place = "Gedung A Ruangan 0210" 
        }
      );

      modelBuilder.Entity<Enrollment>()
      .HasOne(e => e.Student)
      .WithMany(s => s.Enrollments)
      .HasForeignKey(e => e.StudentId);

      modelBuilder.Entity<Enrollment>()
      .HasOne(e => e.Course)
      .WithMany(c => c.Enrollments)
      .HasForeignKey(e => e.CourseId);
    }
  }
}