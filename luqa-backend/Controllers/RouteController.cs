using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using luqa_backend.Models;
using Route = luqa_backend.Models.Route;

namespace luqa_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RouteController : ControllerBase
{
    private readonly LuqaContext _context;

    public RouteController(LuqaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Route>> GetRoutes()
    {
        return await _context.Route.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Route>> GetRoute(int id)
    {
        var route = await _context.Route.FindAsync(id);
        if (route == null)
            return NotFound();
        return route;
    }

    [HttpPost]
    public async Task<ActionResult<Route>> CreateRoute(Route route)
    {
        _context.Route.Add(route);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoute), new { id = route.Id }, route);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRoute(int id, Route route)
    {
        if (id != route.Id)
            return BadRequest();

        _context.Entry(route).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoute(int id)
    {
        var route = await _context.Route.FindAsync(id);
        if (route == null)
            return NotFound();

        _context.Route.Remove(route);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}