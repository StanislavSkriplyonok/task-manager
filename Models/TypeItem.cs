namespace TaskManager.Api.Models;

public class TypeItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Note> Notes { get; set; } = new();
}