using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Services
{
  public class AttendanceService : IAttendanceService
  {
    private readonly ApplicationDbContext _context;

    // Inject DbContext
    public AttendanceService(ApplicationDbContext context)
    {
      _context = context;
    }

    public List<Attendance> GetAllAttendances()
    {
      return _context.Attendances
        .OrderByDescending(a => a.Date)
        .ToList();
    }

    public Attendance GetAttendanceById(int id)
    {
      return _context.Attendances.Find(id);
    }

    public void AddAttendance(Attendance attendance)
    {
      attendance.Date = DateTime.Now;

      _context.Attendances.Add(attendance);
      _context.SaveChanges(); // ← SIMPAN ke database

      Console.WriteLine($"✅ Attendance saved to DB: {attendance.Participant}");
    }

    public void UpdateAttendance(Attendance attendance)
    {
      _context.Attendances.Update(attendance);
      _context.SaveChanges();

      Console.WriteLine($"✅ Attendance updated: {attendance.Participant}");
    }

    public void DeleteAttendance(int id)
    {
      var attendance = _context.Attendances.Find(id);
      if (attendance != null)
      {
        _context.Attendances.Remove(attendance);
        _context.SaveChanges();

        Console.WriteLine($"✅ Attendance deleted: {attendance.Participant}");
      }
    }
  }
}