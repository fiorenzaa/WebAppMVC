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

    [Required(ErrorMessage = "Nama dosen Mata Kuliah harus diisi.")]
    public string Lecturer {get; set;}

    [Required(ErrorMessage = "Jadwal Mata Kuliah harus diisi.")]
    public string Day {get; set;}

    [Required(ErrorMessage = "Jam mulai harus diisi.")]
    public string Start {get; set;}

    [Required(ErrorMessage = "Jam berakhir harus diisi.")]
    public string End {get; set;}

    [Required(ErrorMessage = "Ruangan harus diisi.")]
    public string Place {get; set;}

    // Navigation
    public ICollection<Enrollment> Enrollments { get; set; }
  }
}