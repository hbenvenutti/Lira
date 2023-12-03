using System.Net;
using Lira.Common.Enums;
using Lira.Common.Types;

namespace Lira.Application.Responses;

public class HandlerPagedResponse<T> : HandlerResponse<T>, IPagedResponse
{
    # region ---- properties ---------------------------------------------------

    public uint Page { get; init; }
    public uint PageSize { get; init; }
    public uint TotalRecords { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public HandlerPagedResponse(
        HttpStatusCode httpStatusCode,
        AppStatusCode appStatusCode,
        uint page,
        uint pageSize,
        uint totalRecords,
        T? data = default,
        Errors? errors = null,
        bool isSuccess = false)

        : base(
            httpStatusCode,
            appStatusCode,
            data,
            errors,
            isSuccess
        )
    {
        Page = page;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }

    # endregion
}
