using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class NoteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset CreationTime { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public List<string> Tasks { get; set; } = new();
}

public class CreateNoteDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    public int TypeId { get; set; }
}

public class UpdateNoteDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    public int TypeId { get; set; }
}
