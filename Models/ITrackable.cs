namespace TaskManager.Api.Models;

public interface ITrackable
{
    DateTimeOffset CreationTime { get; set; }
}