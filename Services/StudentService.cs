using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Services
{
  public class StudentService : IStudentService
  {
    private readonly ApplicationDbContext _context;

    // Inject DbContext
    public StudentService(ApplicationDbContext context)
    {
      _context = context;
    }

    public List<Student> GetAllStudents()
    {
      return _context.Students.ToList();
    }

    public Student GetStudentById(int id)
    {
      return _context.Students.Find(id);
    }

    public void AddStudent(Student student)
    {
      _context.Students.Add(student);
      _context.SaveChanges(); // ← PENTING: Simpan ke database
    }

    public void UpdateStudent(Student student)
    {
      _context.Students.Update(student);
      _context.SaveChanges();
    }

    public void DeleteStudent(int id)
    {
      var student = _context.Students.Find(id);
      if (student != null)
      {
        _context.Students.Remove(student);
        _context.SaveChanges();
      }
    }
  }
}