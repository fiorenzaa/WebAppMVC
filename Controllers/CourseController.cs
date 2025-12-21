using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.Data; // Tambahkan ini
using Microsoft.EntityFrameworkCore;
using WebAppMVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal; // Tambahkan ini

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

    // GET: Course/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
      var course = await _context.Course
        .Include(c => c.Enrollments)
        .ThenInclude(e => e.Student)
        .FirstOrDefaultAsync(c => c.Id == id);
      var students = await _context.Students.ToListAsync();

      var vm = new CourseDetailsViewModel
      {
        Course = course,
        Students = course.Enrollments.Select(e => e.Student).ToList()
      };

      return View(vm);
    }

    // GET: Course/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      ViewBag.Days = new SelectList(new[]
      {
        "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
      });
      return View();
    }

    // POST: Course/Create
    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
      if (ModelState.IsValid)
      {
        _context.Course.Add(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      ViewBag.Days = new SelectList(new[]
      {
        "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
      });
      return View(course);
    }

    // GET: Course/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
      var course = await _context.Course.FindAsync(id);
      if (course == null) return NotFound();

      ViewBag.Days = new SelectList(new[]
      {
        "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
      });
      return View(course);
    }

    // POST: Attendance/Edit/{id}
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Course course)
    {
      if (id != course.Id) return NotFound();

      if (ModelState.IsValid)
      {
        _context.Update(course);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      ViewBag.Days = new SelectList(new[]
      {
        "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
      });
      return View(course);
    }

    // GET: Course/Delete/{id}
    public async Task<IActionResult> Delete(int id)
    {
      var course = await _context.Course.FindAsync(id);
      if(course == null) return NotFound();

      return View(course);
    }

    // POST: Course/Delete/{id}
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> Delete(int id, Course course)
    {
      // var course = await _context.Course.FindAsync();
      if(course != null)
      {
        _context.Course.Remove(course);
      }
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    //GET: Enrollment
    public async Task<IActionResult> Enrollment(int id)
    {
      var course = await _context.Course.FindAsync(id);
      if (course == null) return NotFound();

      var students = await _context.Students.ToListAsync();

      var vm = new CourseEnrollmentViewModel
      {
        Course = course,
        Students = students,
        SelectedStudentIds = new List<int>()
      };

      return View(vm);
    }

    //POST: Enrollment
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enrollment(CourseEnrollmentViewModel vm)
    {
      if (vm.SelectedStudentIds != null)
      {
        foreach (var studentId in vm.SelectedStudentIds)
        {
          bool alreadyEnrolled = await _context.Enrollments.AnyAsync(e =>
              e.CourseId == vm.Course.Id && e.StudentId == studentId);

          if (!alreadyEnrolled)
          {
            _context.Enrollments.Add(new Enrollment
            {
              CourseId = vm.Course.Id,
              StudentId = studentId
            });
          }
        }

        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Details), new { id = vm.Course.Id });
    }
  }
}