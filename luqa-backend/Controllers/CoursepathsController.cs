using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CoursepathsController : ControllerBase
{
    private readonly LuqaContext _context;

    public CoursepathsController(LuqaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coursepaths>>> GetCoursepaths()
    {
        return await _context.Coursepaths
            .Include(cp => cp.Route)
            .Include(cp => cp.Course)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Coursepaths>> GetCoursepath(int id)
    {
        var coursepath = await _context.Coursepaths
            .Include(cp => cp.Route)
            .Include(cp => cp.Course)
            .FirstOrDefaultAsync(cp => cp.Id == id);

        if (coursepath == null)
            return NotFound();

        return coursepath;
    }

    [HttpPost]
    public async Task<ActionResult<Coursepaths>> CreateCoursepath(Coursepaths cp)
    {
        _context.Coursepaths.Add(cp);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCoursepath), new { id = cp.Id }, cp);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCoursepath(int id, Coursepaths cp)
    {
        if (id != cp.Id)
            return BadRequest();

        _context.Entry(cp).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoursepath(int id)
    {
        var cp = await _context.Coursepaths.FindAsync(id);
        if (cp == null)
            return NotFound();

        _context.Coursepaths.Remove(cp);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}