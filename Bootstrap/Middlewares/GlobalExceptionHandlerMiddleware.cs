using System.Net;
using System.Text.Json;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Common.Structs;
using Microsoft.AspNetCore.Http;

namespace Lira.Bootstrap.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    # region ---- properties ---------------------------------------------------

    private readonly RequestDelegate _next;

    # endregion

    # region ---- constructor --------------------------------------------------

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    # endregion

    # region ---- invoke -------------------------------------------------------

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }

        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    # endregion

    # region ---- exception handlers -------------------------------------------

    private static Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception exception
    )
    {
        var messages = exception.InnerException is null
            ? new List<string> { exception.Message }
            : new List<string> { exception.Message, exception.InnerException.Message };

        var response = new Response<object>(
            HttpStatusCode.InternalServerError,
            StatusCode.UnexpectedError,
            error: new ErrorDto(messages)
        );

        httpContext.Response.ContentType = HttpContentTypes.Json;
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return httpContext.Response
            .WriteAsync(text: JsonSerializer.Serialize(response));
    }

    # endregion
}
