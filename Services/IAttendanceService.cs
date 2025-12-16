using WebAppMVC.Models;

namespace WebAppMVC.Services
{
  public interface IAttendanceService
  {
    List<Attendance> GetAllAttendances();
    Attendance GetAttendanceById(int id);
    void AddAttendance(Attendance attendance);
    void UpdateAttendance(Attendance attendance);
    void DeleteAttendance(int id);
    }
}