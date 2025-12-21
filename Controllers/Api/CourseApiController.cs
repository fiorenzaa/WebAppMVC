using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers.Api
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v1/CoursesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesV1()
        {
            return await _context.Course.ToListAsync();
        }

        // GET: api/v1/CoursesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseV1(int id)
        {
            var course = await _context.Course.FindAsync(id);
            
            if (course == null)
            {
                return NotFound();
            }
            
            return course;
        }

        // POST: api/v1/CoursesApi
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourseV1(Course course)
        {
            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourseV1), new { id = course.Id }, course);
        }

        // PUT: api/v1/CoursesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseV1(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/v1/CoursesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseV1(int id)
        {
            var course = await _context.Course.FindAsync(id);
            
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }
    }

    //__________________________________________________________________
    // Versi 2.0 (Optional - jika ada perubahan)
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CoursesApiV2Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesApiV2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/v2/CoursesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetCoursesV2()
        {
            // Contoh: Di V2, tambahkan informasi jumlah student yang terdaftar
            return await _context.Course
                .Select(c => new 
                { 
                    c.Id, 
                    c.CourseName, 
                    c.Lecturer,
                })
                .ToListAsync();
        }
    }
}