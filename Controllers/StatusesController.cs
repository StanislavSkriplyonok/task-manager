using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Mapping;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusesController: ControllerBase
{
    private readonly AppDbContext _context;

    public StatusesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/statuses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
    {
        var statuses = await _context.Statuses
            .OrderBy(s => s.Id)
            .ToListAsync();
        return statuses.Select(s => s.ToDto()).ToList();
    }

    // GET: api/statuses/1
    [HttpGet("{id}")]
    public async Task<ActionResult<StatusDto>> GetStatus(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if (status == null) return NotFound();
        return status.ToDto();
    }

    // POST: api/statuses
    [HttpPost]
    public async Task<ActionResult<StatusDto>> CreateStatus(CreateStatusDto dto)
    {
        var status = dto.ToEntity();

        _context.Statuses.Add(status);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status.ToDto());
    }

    // PUT: api/statuses/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateStatusDto dto)
    {
        var status = await _context.Statuses.FindAsync(id);
        if(status == null) return NotFound();

        status.Name = dto.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/statuses/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStatus(int id)
    {
        var status = await _context.Statuses.FindAsync(id);
        if(status == null) return NotFound();

        _context.Statuses.Remove(status);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}