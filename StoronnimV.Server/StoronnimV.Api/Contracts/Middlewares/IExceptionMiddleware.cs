using System.Net;

namespace StoronnimV.Api.Contracts.Middlewares;

public interface IExceptionMiddleware
{
    public Task InvokeAsync(HttpContext context);
    public Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex);
    public Task HandleExceptionAsync(HttpContext context, int statusCode, Exception ex);
}