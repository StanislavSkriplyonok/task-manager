namespace TaskManager.Api.Models;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty; 
    public DateTimeOffset CreationTime { get; set; }

    // Foreign key to the note type
    public int NoteTypeId { get; set; }
    public NoteType NoteType { get; set; } = null;
}