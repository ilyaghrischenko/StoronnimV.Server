using System.Net;

namespace StoronnimV.Api.Interfaces.Middlewares;

public interface IExceptionMiddleware
{
    public Task InvokeAsync(HttpContext context);
    public Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message);
}