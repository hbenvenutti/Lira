using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;

namespace Lira.Application.Responses;

public readonly struct Response<T> : IHandlerResponse<T> where T : struct
{
    # region ---- properties ---------------------------------------------------

    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public ErrorDto? Error { get; init; }
    public HttpStatusCode HttpStatusCode { get; init; }
    public AppStatusCode AppStatusCode { get; init; }
    public PaginationDto? Pagination { get; init; }

    # endregion

    # region ---- constructors -------------------------------------------------

    public Response(
        HttpStatusCode httpStatusCode,
        AppStatusCode appStatusCode,
        bool isSuccess = false,
        PaginationDto? pagination = null,
        ErrorDto? error = null,
        T? data = default
    )
    {
        HttpStatusCode = httpStatusCode;
        AppStatusCode = appStatusCode;
        Pagination = pagination;
        Error = error;
        IsSuccess = isSuccess;
        Data = data;
    }

    # endregion
}
