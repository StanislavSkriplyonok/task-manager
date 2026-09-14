using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Mapping;

public static class TaskItemMapper
{
    public static TaskItemDto ToDto(this TaskItem task)
    {
        return new TaskItemDto
        {
            Id = task.Id,
            Description = task.Description,
            CreationTime = task.CreationTime,
            NoteName = task.Note.Name,
            StatusName = task.Status.Name,
        };
    }

    public static TaskItem ToEntity(this CreateTaskItemDto dto)
    {
        return new TaskItem
        {
            Description = dto.Description,
            NoteId = dto.NoteId,
            CreationTime = DateTimeOffset.UtcNow,
            StatusId = 1
        };
    }

    public static void UpdateEntity(this TaskItem task, UpdateTaskItemDto dto)
    {
        task.Description = dto.Description;
        task.StatusId = dto.StatusId;
    }
}