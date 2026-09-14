using TaskManager.Api.DTOs;
using TaskManager.Api.Models;

namespace TaskManager.Api.Mapping;

public static class StatusMapper
{
    public static StatusDto ToDto(this Status status)
    {
        return new StatusDto
        {
            Id = status.Id,
            Name = status.Name
        };
    }
    public static Status ToEntity(this CreateStatusDto dto)
    {
        return new Status
        {
            Name = dto.Name
        };
    }
}