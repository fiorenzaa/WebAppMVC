using System.ComponentModel.DataAnnotations;
namespace WebAppMVC.Models
{
  public class Attendance
  {
    public int Id {get; set;}
    public string Participant {get; set;}
    public DateTime Date {get; set;}
    public string Status {get; set;}
    public string Notes {get; set;} = string.Empty;

    // public string FormattedDateTime => Date.ToString("dd/MM/yyyy/ HH:mm:ss");
  }
}