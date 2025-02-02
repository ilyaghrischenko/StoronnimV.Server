using System.Net;

namespace StoronnimV.Api.Contracts.Middlewares;

public interface IExceptionMiddleware
{
    public Task InvokeAsync(HttpContext context);
    public Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message);
    public Task HandleExceptionAsync(HttpContext context, int statusCode, string message);
}