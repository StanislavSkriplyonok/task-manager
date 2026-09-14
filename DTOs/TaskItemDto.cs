using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class TaskItemDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreationTime { get; set; }
    public string NoteName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
}

public class CreateTaskItemDto
{
    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Note is required")]
    public int NoteId { get; set; }
}

public class UpdateTaskItemDto
{
    [Required(ErrorMessage = "Description is required")]
    [StringLength(250, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status is required")]
    public int StatusId { get; set; }
}
