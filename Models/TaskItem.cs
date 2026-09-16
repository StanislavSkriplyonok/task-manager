namespace TaskManager.Api.Models;

public class TaskItem: ITrackable
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreationTime { get; set; }

    // Foreign key to the note
    public int NoteId { get; set; }
    public Note Note { get; set; } = null!;

    // Foreign key to the status
    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;
}