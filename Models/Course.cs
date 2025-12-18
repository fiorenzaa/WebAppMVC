using System.ComponentModel.DataAnnotations;
namespace WebAppMVC.Models
{
  public class Course
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama Mata Kuliah harus diisi.")]
    [StringLength(100, ErrorMessage = "Nama Mata Kuliah tidak lebih dari 100 karakter.")]
    public string CourseName { get; set; }

    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; }
  }
}