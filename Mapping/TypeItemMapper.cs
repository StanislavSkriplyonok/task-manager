using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Mapping;

public static class TypeItemMapper
{
    public static TypeItemDto ToDto(this TypeItem typeItem)
    {
        return new TypeItemDto
        {
            Id = typeItem.Id,
            Name = typeItem.Name
        };
    }
    public static TypeItem ToEntity(this CreateTypeItemDto dto)
    {
        return new TypeItem
        {
            Name = dto.Name
        };
    }
}