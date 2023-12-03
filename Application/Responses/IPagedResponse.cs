namespace Lira.Application.Responses;

public interface IPagedResponse
{
    uint Page { get; init; }
    uint PageSize { get; init; }
    uint TotalRecords { get; init; }
}
