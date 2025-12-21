using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models;
using WebAppMVC.Data; // Tambahkan ini
using Microsoft.EntityFrameworkCore; // Tambahkan ini

namespace WebAppMVC.Controllers
{
  public class AttendanceController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AttendanceController(ApplicationDbContext context)
    {
      _context = context;
    }

    //GET: Attendance
    public async Task<IActionResult> Index()
    {
      return View(await _context.Attendances.ToListAsync());
    }

    // GET: Attendance/Details/{id}
    public async Task<IActionResult> Details(int id)
    {
      var attendance = await _context.Attendances.FindAsync(id);
      if (attendance == null) return NotFound();

      return View(attendance);
    }

    // GET: Attendance/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
      // Ambil semua students
      var students = await _context.Students.ToListAsync();
      // Kirim ke View menggunakan ViewBag atau ViewData
      ViewBag.Students = new SelectList(students, "Name", "Name");
      // Format: SelectList(data, valueField, textField)
      return View();
    }

    // POST: Attendance/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Attendance attendance)
    {
      if (ModelState.IsValid)
      {
        _context.Attendances.Add(attendance);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      // Reload dropdown kalau error
      ViewBag.Students = new SelectList(
          await _context.Students.ToListAsync(), "Name", "Name");

      return View(attendance);
    }

    // GET: Attendance/Edit/{id}
    public async Task<IActionResult> Edit(int id)
    {
      var attendance = await _context.Attendances.FindAsync(id);
      if (attendance == null) return NotFound();

      ViewBag.Students = new SelectList(
          await _context.Students.ToListAsync(), "Name", "Name");

      return View(attendance);
    }

    // POST: Attendance/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Attendance attendance)
    {
      if (id != attendance.Id) return NotFound();

      if (ModelState.IsValid)
      {
        _context.Update(attendance);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }

      ViewBag.Students = new SelectList(
          await _context.Students.ToListAsync(), "Name", "Name");

      return View(attendance);
    }

    // GET: Attendance/Delete/{id}
    public async Task<IActionResult> Delete(int id)
    {
      var attendance = await _context.Attendances.FindAsync(id);
      if (attendance == null) return NotFound();

      return View(attendance);
    }

    // POST: Student/Delete/{id}
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var attendance = await _context.Attendances.FindAsync(id);
      if (attendance != null)
      {
        _context.Attendances.Remove(attendance);
      }
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }
  }
}