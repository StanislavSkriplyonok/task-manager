using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.DTOs;
using TaskManager.Api.Models;
using TaskManager.Api.Mapping;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypeItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TypeItemsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/types
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypeItemDto>>> GetTypeItems()
    {
        var types = await _context.Types
            .OrderBy(t => t.Id)
            .ToListAsync();
        return types.Select(t => t.ToDto()).ToList();
    }

    // GET: api/types/1
    [HttpGet("{id}")]
    public async Task<ActionResult<TypeItemDto>> GetTypeItem(int id)
    {
        var type = await _context.Types.FindAsync(id);
        if (type == null) return NotFound();
        return type.ToDto();
    }

    // POST: api/types
    [HttpPost]
    public async Task<ActionResult<TypeItemDto>> CreateTypeItem(CreateTypeItemDto dto)
    {
        var type = dto.ToEntity();

        _context.Types.Add(type);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTypeItem), new { id = type.Id }, type.ToDto());
    }

    // PUT: api/types/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTypeItem(int id, UpdateTypeItemDto dto)
    {
        var type = await _context.Types.FindAsync(id);
        if (type == null) return NotFound();

        type.Name = dto.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/types/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTypeItem(int id)
    {
        var type = await _context.Types.FindAsync(id);
        if (type == null) return NotFound();

        _context.Types.Remove(type);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}