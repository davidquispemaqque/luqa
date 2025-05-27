using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize] // Todas las rutas protegidas con JWT
public class CourseController : ControllerBase
{
    private readonly LuqaContext _context;

    public CourseController(LuqaContext context)
    {
        _context = context;
    }

    // GET: api/Course
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Course.ToListAsync();
    }

    // GET: api/Course/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _context.Course.FindAsync(id);
        if (course == null)
            return NotFound();
        return course;
    }

    // POST: api/Course
    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        _context.Course.Add(course);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    // PUT: api/Course/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, Course course)
    {
        if (id != course.Id)
            return BadRequest();

        _context.Entry(course).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Course/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Course.FindAsync(id);
        if (course == null)
            return NotFound();

        _context.Course.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}