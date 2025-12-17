using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.Data; // Tambahkan ini
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels; // Tambahkan ini

namespace WebAppMVC.Controllers
{
  public class CourseController : Controller
  {
    private readonly ApplicationDbContext _context; 

    // Dependency Injection melalui konstruktor
    public CourseController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Course
    public async Task<IActionResult> Index()
    {
      return View(await _context.Course.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
      var course = await _context.Course.FindAsync(id);
      var students = await _context.Students.ToListAsync();

      var vm = new CourseDetailsViewModel
      {
        Course = course,
        Students = students
      };

      return View(vm);
    }
  }
}