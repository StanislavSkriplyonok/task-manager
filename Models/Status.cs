namespace TaskManager.Api.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<TaskItem> Tasks { get; set; } = new();
}