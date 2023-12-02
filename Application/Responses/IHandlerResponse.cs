using System.Net;
using Lira.Application.Enums;

namespace Lira.Application.Responses;

public interface IHandlerResponse<T>
{
    bool IsSuccess { get; init; }
    HttpStatusCode HttpStatusCode { get; init; }
    AppStatusCode AppStatusCode { get; init; }
    T? Data { get; init; }
    List<string>? Errors { get; init; }
}
