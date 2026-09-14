using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class StatusDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateStatusDto
{
    [Required(ErrorMessage = "Status name is required")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Status name must be between 2 and 25 characters")]
    public string Name { get; set; } = string.Empty;
}

public class UpdateStatusDto
{
    [Required(ErrorMessage = "Status name is required")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Status name must be between 2 and 25 characters")]
    public string Name { get; set; } = string.Empty;
}
