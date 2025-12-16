using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models;
using WebAppMVC.Services;

namespace WebAppMVC.Controllers
{
  public class AttendanceController : Controller
  {
    private readonly IAttendanceService _attendanceService;
    private readonly IStudentService _studentService;

    public AttendanceController(
      IStudentService studentService,
      IAttendanceService attendanceService)
    {
      _studentService = studentService;
      _attendanceService = attendanceService;
    }

    //GET: Attendance
    public IActionResult Index()
    {
      var attendances = _attendanceService.GetAllAttendances();
      return View(attendances);
    }

    // GET: Attendance/Create
    public IActionResult Create()
    {
      // Ambil semua students
      var students = _studentService.GetAllStudents();
      // Kirim ke View menggunakan ViewBag atau ViewData
      ViewBag.Students = new SelectList(students, "Name", "Name");
      // Format: SelectList(data, valueField, textField)

      return View();
    }

    // POST: Attendance/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Attendance attendance)
    {
      if (ModelState.IsValid)
      {
        ViewBag.Message = "Data presensi berhasil disimpan!";
        _attendanceService.AddAttendance(attendance);
        return RedirectToAction(nameof(Index)); // Redirect ke daftar siswa
      }

      return View(attendance);
    }

    // GET: Attendance/Edit/{id}
    public IActionResult Edit(int id)
    {
      var attendance = _attendanceService.GetAttendanceById(id);
      var students = _studentService.GetAllStudents();
      // Kirim ke View menggunakan ViewBag atau ViewData
      ViewBag.Students = new SelectList(students, "Name", "Name");
      if (attendance == null)
      {
        return NotFound();
      }
      return View(attendance);
    }

    // POST: Attendance/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Attendance attendance)
    {
      if (id != attendance.Id)
      {
        return NotFound();
      }
      if (ModelState.IsValid)
      {
        _attendanceService.UpdateAttendance(attendance);
        return RedirectToAction(nameof(Index));
      }
      return View(attendance);
    }

    // GET: Attendance/Delete/{id}
    public IActionResult Delete(int id)
    {
      var attendance = _attendanceService.GetAttendanceById(id);
      if (attendance == null)
      {
        return NotFound();
      }
      return View(attendance);
    }

    // GET: Attendance/Details/{id}
    public IActionResult Details(int id)
    {
      var attendance = _attendanceService.GetAttendanceById(id);
      if (attendance == null)
      {
        return NotFound();
      }
      return View(attendance);
    }
  }
}