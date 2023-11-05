namespace Lira.Application.Dto;

public class PaginationDto
{
    public uint Page { get; init; }
    public uint PageSize { get; init; }
    public uint Total { get; init; }
}
