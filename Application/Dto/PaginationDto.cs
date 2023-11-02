namespace Lira.Application.Dto;

public readonly struct PaginationDto
{
    public uint Page { get; init; }
    public uint PageSize { get; init; }
    public uint Total { get; init; }
}
