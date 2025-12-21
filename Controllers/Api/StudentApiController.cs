using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers.Api
{
  [ApiVersion("1.0")] // Menentukan versi API untuk controller ini
  [Route("api/v{version:apiVersion}/[controller]")]  // Untuk URI versioning
  [Route("api/[controller]")] // Untuk Query String versioning
  [ApiController] // Atribut yang menyediakan perilaku khusus API (misalnya, validasi model otomatis)
  public class StudentApiController : ControllerBase // Warisi dari ControllerBase untuk API
  {
    private readonly ApplicationDbContext _context;
    public StudentApiController(ApplicationDbContext context)
    {
      _context = context;
    }
    // GET: api/v1/StudentsApi
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsV1()
    {
      return await _context.Students.ToListAsync();
    }

    // GET: api/v1/StudentsApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudentV1(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      return student;
    }

    // PUT: api/v1/StudentsApi/5
    // Untuk mengupdate seluruh resource
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudentV1(int id, Student student)
    {
      if (id != student.Id)
      {
        return BadRequest();
      }
      _context.Entry(student).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StudentExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent(); // 204 No Content
    }

    // POST: api/v1/StudentsApi
    // Untuk membuat resource baru
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudentV1(Student student)
    {
      _context.Students.Add(student);
      await _context.SaveChangesAsync();
      // Mengembalikan 201 Created dengan lokasi resource baru
      return CreatedAtAction("GetStudent", new { id = student.Id }, student);
    }

    // DELETE: api/StudentsApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentV1(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound();
      }
      _context.Students.Remove(student);
      await _context.SaveChangesAsync();
      return NoContent(); // 204 No Content
    }

    private bool StudentExists(int id)
    {
      return _context.Students.Any(e => e.Id == id);
    }

    // PATCH: api/v1/StudentsApi/5
    // Untuk mengupdate sebagian resource menggunakan JSON Patch
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchStudentV1(int id, [FromBody] JsonPatchDocument<Student> patchDoc)
    {
      if (patchDoc == null)
      {
        return BadRequest("Patch document is null");
      }

      var student = await _context.Students.FindAsync(id);

      if (student == null)
      {
        return NotFound();
      }

      // Terapkan perubahan dari patch document
      patchDoc.ApplyTo(student, ModelState);

      // Validasi model setelah patch diterapkan
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      // Validasi manual jika diperlukan
      if (!TryValidateModel(student))
      {
        return BadRequest(ModelState);
      }

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!StudentExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent(); // 204 No Content
    }

    //__________________________________________________________________
    // Contoh Controller untuk versi 2.0 (jika ada perubahan signifikan)
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]  // Untuk URI versioning
    [Route("api/[controller]")] // Untuk Query String versioning
    [ApiController]
    public class StudentsApiV2Controller : ControllerBase
    {
      private readonly ApplicationDbContext _context;
      public StudentsApiV2Controller(ApplicationDbContext context)
      {
        _context = context;
      }
      // GET: api/v2/StudentsApi
      [HttpGet]
      public async Task<ActionResult<IEnumerable<Student>>> GetStudentsV2()
      {
        // Misalkan di V2, kita hanya mengembalikan nama dan email
        return await _context.Students
        .Select(s => new Student { Id = s.Id, Name = s.Name, Email = s.Email })
        .ToListAsync();
      }
    }
  }
}