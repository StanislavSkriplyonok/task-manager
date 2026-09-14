namespace TaskManager.Api.Models;

public class Note
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public DateTimeOffset CreationTime { get; set; }

    // Foreign key to the note type
    public int TypeId { get; set; }
    public TypeItem Type { get; set; } = null!;
    public List<TaskItem> Tasks { get; set; } = new();
}