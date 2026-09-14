using System.ComponentModel.DataAnnotations;

namespace TaskManager.Api.DTOs;

public class TypeItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateTypeItemDto
{
    [Required(ErrorMessage = "Type name is required")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Type name must be between 2 and 25 characters")]
    public string Name { get; set; } = string.Empty;
}

public class UpdateTypeItemDto
{
    [Required(ErrorMessage = "Type name is required")]
    [StringLength(25, MinimumLength = 2, ErrorMessage = "Type name must be between 2 and 25 characters")]
    public string Name { get; set; } = string.Empty;
}
