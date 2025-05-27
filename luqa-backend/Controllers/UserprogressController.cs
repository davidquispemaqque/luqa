using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserprogressController : ControllerBase
{
    private readonly LuqaContext _context;

    public UserprogressController(LuqaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Userprogress>>> GetUserProgress()
    {
        return await _context.Userprogress
            .Include(up => up.User)
            .Include(up => up.Lesson)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Userprogress>> GetUserProgress(int id)
    {
        var progress = await _context.Userprogress
            .Include(up => up.User)
            .Include(up => up.Lesson)
            .FirstOrDefaultAsync(up => up.Id == id);

        if (progress == null)
            return NotFound();

        return progress;
    }

    [HttpPost]
    public async Task<ActionResult<Userprogress>> CreateUserProgress(Userprogress progress)
    {
        progress.ProgressDate = DateTime.Now;
        _context.Userprogress.Add(progress);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserProgress), new { id = progress.Id }, progress);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserProgress(int id, Userprogress progress)
    {
        if (id != progress.Id)
            return BadRequest();

        _context.Entry(progress).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserProgress(int id)
    {
        var progress = await _context.Userprogress.FindAsync(id);
        if (progress == null)
            return NotFound();

        _context.Userprogress.Remove(progress);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
