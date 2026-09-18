using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Mapping;
using TaskManager.Api.Models;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/notes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes()
    {
        var notes = await _context.Notes
            .Include(n => n.Type)
            .Include(n => n.Tasks)
            .OrderBy(n => n.Id)
            .ToListAsync();
        return notes.Select(t => t.ToDto()).ToList();
    }

    // GET: api/notes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNote(int id)
    {
        var note = await _context.Notes
            .Include(n => n.Type)
            .Include(n => n.Tasks)
            .FirstOrDefaultAsync(n => n.Id == id);
        if (note == null) return NotFound();
        return note.ToDto();
    }

    // POST: api/notes
    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote(CreateNoteDto dto)
    {
        var typeExists = await _context.Types.AnyAsync(t => t.Id == dto.TypeId);
        if (!typeExists)
            return BadRequest("Type not found");

        var note = dto.ToEntity();

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        await _context.Entry(note).Reference(n => n.Type).LoadAsync();

        return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note.ToDto());
    }

    // PUT: api/notes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, UpdateNoteDto dto)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return NotFound();

        var typeExists = await _context.Types.AnyAsync(t => t.Id == dto.TypeId);
        if (!typeExists)
            return BadRequest("Type not found");

        note.UpdateEntity(dto);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/notes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null) return NotFound();

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}