using System.Net;
using Lira.Common.Enums;
using Lira.Common.Types;

namespace Lira.Application.Responses;

public class HandlerResponse<T> : IHandlerResponse<T>
{
    # region ---- properties ---------------------------------------------------

    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
    public HttpStatusCode HttpStatusCode { get; init; }
    public AppStatusCode AppStatusCode { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public HandlerResponse(
        HttpStatusCode httpStatusCode,
        AppStatusCode appStatusCode,
        T? data = default,
        Errors? errors = null,
        bool isSuccess = false
    )
    {
        IsSuccess = isSuccess;
        Data = data;
        Errors = errors;
        HttpStatusCode = httpStatusCode;
        AppStatusCode = appStatusCode;
    }

    # endregion
}
