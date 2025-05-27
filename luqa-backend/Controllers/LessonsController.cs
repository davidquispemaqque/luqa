using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LessonsController : ControllerBase
{
    private readonly LuqaContext _context;

    public LessonsController(LuqaContext context)
    {
        _context = context;
    }

    // GET: api/Lessons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lessons>>> GetLessons()
    {
        return await _context.Lessons
            .Include(l => l.Course)
            .ToListAsync();
    }

    // GET: api/Lessons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Lessons>> GetLesson(int id)
    {
        var lesson = await _context.Lessons
            .Include(l => l.Course)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null)
            return NotFound();

        return lesson;
    }

    // POST: api/Lessons
    [HttpPost]
    public async Task<ActionResult<Lessons>> CreateLesson(Lessons lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLesson), new { id = lesson.Id }, lesson);
    }

    // PUT: api/Lessons/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLesson(int id, Lessons lesson)
    {
        if (id != lesson.Id)
            return BadRequest();

        _context.Entry(lesson).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Lessons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {
        var lesson = await _context.Lessons.FindAsync(id);
        if (lesson == null)
            return NotFound();

        _context.Lessons.Remove(lesson);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}