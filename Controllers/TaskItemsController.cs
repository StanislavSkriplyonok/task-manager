using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Mapping;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TaskItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/types
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTaskItems()
    {
        var tasks = await _context.Tasks
            .Include(t => t.Note)
            .Include(t => t.Status)
            .OrderBy(t => t.Id)
            .ToListAsync();
        return tasks.Select(t => t.ToDto()).ToList();
    }

    // GET: api/types/1
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItemDto>> GetTaskItem(int id)
    {
        var task = await _context.Tasks
            .Include(t => t.Note)
            .Include(t => t.Status)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (task == null) return NotFound();
        return task.ToDto();
    }

    // POST: api/types
    [HttpPost]
    public async Task<ActionResult<TaskItemDto>> CreateTaskItem(CreateTaskItemDto dto)
    {
        var noteExists = await _context.Notes.AnyAsync(n => n.Id == dto.NoteId);
        if (!noteExists)
            return BadRequest("Note not found");

        var defaultStatus = await _context.Statuses.FindAsync(1);
        if (defaultStatus == null)
            return Problem("Default status (Id=1) is missing in the database");

        var task = dto.ToEntity();

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        await _context.Entry(task).Reference(t => t.Note).LoadAsync();
        await _context.Entry(task).Reference(t => t.Status).LoadAsync();

        return CreatedAtAction(nameof(GetTaskItem), new { id = task.Id }, task.ToDto());
    }

    // PUT: api/types/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTaskItem(int id, UpdateTaskItemDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        var statusExists = await _context.Statuses.AnyAsync(s => s.Id == dto.StatusId);
        if (!statusExists)
            return BadRequest("Status not found");

        task.Description = dto.Description;
        task.StatusId = dto.StatusId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/types/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskItem(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}