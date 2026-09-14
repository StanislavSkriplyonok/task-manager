using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Mapping;

public static class NoteMapper
{
    public static NoteDto ToDto(this Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            Name = note.Name,
            CreationTime = note.CreationTime,
            TypeName = note.Type.Name,
            Tasks = note.Tasks.Select(t => t.Description).ToList(),
        };
    }

    public static Note ToEntity(this CreateNoteDto dto)
    {
        return new Note
        {
            Name = dto.Name,
            TypeId = dto.TypeId,
            CreationTime = DateTimeOffset.UtcNow,
        };
    }

    public static void UpdateEntity(this Note note, UpdateNoteDto dto)
    {
        note.Name = dto.Name;
        note.TypeId = dto.TypeId;
    }
}