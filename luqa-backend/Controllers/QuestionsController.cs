using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController : ControllerBase
{
    private readonly LuqaContext _context;

    public QuestionsController(LuqaContext context)
    {
        _context = context;
    }

    // GET: api/Questions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
    {
        return await _context.Questions
            .Include(q => q.Lesson)
            .ToListAsync();
    }

    // GET: api/Questions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Questions>> GetQuestion(int id)
    {
        var question = await _context.Questions
            .Include(q => q.Lesson)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null)
            return NotFound();

        return question;
    }

    // POST: api/Questions
    [HttpPost]
    public async Task<ActionResult<Questions>> CreateQuestion(Questions question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
    }

    // PUT: api/Questions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuestion(int id, Questions question)
    {
        if (id != question.Id)
            return BadRequest();

        _context.Entry(question).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Questions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question == null)
            return NotFound();

        _context.Questions.Remove(question);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
