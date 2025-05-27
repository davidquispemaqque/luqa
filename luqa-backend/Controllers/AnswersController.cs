using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AnswersController : ControllerBase
{
    private readonly LuqaContext _context;

    public AnswersController(LuqaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Answers>>> GetAnswers()
    {
        return await _context.Answers
            .Include(a => a.Question)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Answers>> GetAnswer(int id)
    {
        var answer = await _context.Answers
            .Include(a => a.Question)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (answer == null)
            return NotFound();

        return answer;
    }

    [HttpPost]
    public async Task<ActionResult<Answers>> CreateAnswer(Answers answer)
    {
        _context.Answers.Add(answer);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, answer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnswer(int id, Answers answer)
    {
        if (id != answer.Id)
            return BadRequest();

        _context.Entry(answer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnswer(int id)
    {
        var answer = await _context.Answers.FindAsync(id);
        if (answer == null)
            return NotFound();

        _context.Answers.Remove(answer);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}