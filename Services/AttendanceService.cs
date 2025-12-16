using WebAppMVC.Models;

namespace WebAppMVC.Services
{
  public class AttendanceService : IAttendanceService
  {
    private static List<Attendance> _attendances = new List<Attendance>
    {
        new () 
        { 
            Id = 1, 
            Participant = "Alice", 
            Date = DateTime.Today, 
            Status = "Hadir", 
            Notes = "Tepat waktu" 
        },
        new () 
        { 
            Id = 2, 
            Participant = "Bob", 
            Date = DateTime.Today, 
            Status = "Sakit", 
            Notes = "Ada surat dokter" 
        }
    };

    public void AddAttendance(Attendance attendance)
    {
      attendance.Id = _attendances.Any()
        ? _attendances.Max(a => a.Id) + 1
        : 1;

      _attendances.Add(attendance);
    }

    public List<Attendance> GetAllAttendances()
    {
      return _attendances;
    }

    public Attendance GetAttendanceById(int id)
    {
      return _attendances.FirstOrDefault(a => a.Id == id);
    }

    public void UpdateAttendance (Attendance attendance)
    {
      var existing = _attendances.FirstOrDefault(a => a.Id == attendance.Id);
      if (existing != null)
      {
        existing.Participant = attendance.Participant;
        existing.Date = attendance.Date;
        existing.Status = attendance.Status;
        existing.Notes = attendance.Notes;
      }
    }

    public void DeleteAttendance(int id)
    {
      var attendance = _attendances.FirstOrDefault(a => a.Id == id);
      if (attendance != null)
      {
        _attendances.Remove(attendance);
      }
    }
  }
}