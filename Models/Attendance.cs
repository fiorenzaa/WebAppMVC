using System.ComponentModel.DataAnnotations;
namespace WebAppMVC.Models
{
  public class Attendance
  {
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Participant harus diisi.")]
    [StringLength(100, ErrorMessage = "Participant tidak boleh lebih dari 100 karakter.")]
    public string Participant { get; set; }

    [Required]
    [Display(Name = "Tanggal")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Status harus dipilih")]
    public string Status { get; set; }

    [StringLength(100, ErrorMessage = "Notes tidak boleh lebih dari 100 karakter.")]
    [Display(Name = "Catatan")]
    public string? Notes { get; set; } = string.Empty;

    // public string FormattedDateTime => Date.ToString("dd/MM/yyyy/ HH:mm:ss");
  }
}