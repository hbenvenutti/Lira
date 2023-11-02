using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;

namespace Lira.Application.Responses;

public readonly struct Response<T> where T : class
{
    # region ---- properties ---------------------------------------------------

    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public ErrorDto? Error { get; init; }
    public HttpStatusCode HttpStatusCode { get; init; }
    public StatusCode StatusCode { get; init; }
    public PaginationDto? Pagination { get; init; }

    # endregion

    # region ---- constructors -------------------------------------------------

    public Response(
        HttpStatusCode httpStatusCode,
        StatusCode statusCode,
        bool isSuccess = false,
        PaginationDto? pagination = null,
        ErrorDto? error = null,
        T? data = null
    )
    {
        HttpStatusCode = httpStatusCode;
        StatusCode = statusCode;
        Pagination = pagination;
        Error = error;
        IsSuccess = isSuccess;
        Data = data;
    }

    # endregion
}
