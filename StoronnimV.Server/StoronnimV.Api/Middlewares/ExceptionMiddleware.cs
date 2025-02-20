using System.Net;
using StoronnimV.Api.Contracts.Middlewares;
using StoronnimV.Application.Exceptions;

namespace StoronnimV.Api.Middlewares;

/// <summary>
/// Middleware для обработки каждой ошибки. Он позволяет отлавливать ошибку в любом месте сервера,
/// обрабатывать, и возвращать соответсвенный статус код и сообщение
/// </summary>
public class ExceptionMiddleware : IExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException ex)
        {
            await HandleExceptionAsync(context,
                499,
                ex);
        }
        catch (EntityNotFoundException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.NotFound,
                ex);
        }
        catch (PaginationException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.BadRequest,
                ex);
        }
        catch (LogInException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.Unauthorized,
                ex);
        }
        catch (PhotoResizingException ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.UnsupportedMediaType,
                ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context,
                HttpStatusCode.InternalServerError,
                ex);
        }
    }

    public async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex)
    {
        string methodName = ex.TargetSite?.Name ?? "UnknownMethod";
        string className = ex.TargetSite?.DeclaringType?.FullName ?? "UnknownClass";
        
        string logMessage = $"EXCEPTION - {methodName}: {ex.Message} (Method: {className}.{methodName})";
        _logger.LogError(logMessage);
        
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "text/plain";
        
        await context.Response.WriteAsync(ex.Message);
    }

    public async Task HandleExceptionAsync(HttpContext context, int statusCode, Exception ex)
    {
        string methodName = ex.TargetSite?.Name ?? "UnknownMethod";
        string className = ex.TargetSite?.DeclaringType?.FullName ?? "UnknownClass";
        
        string logMessage = $"EXCEPTION - {methodName}: {ex.Message} (Method: {className}.{methodName})";
        _logger.LogError(logMessage);
        
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "text/plain";
        
        await context.Response.WriteAsync(ex.Message);
    }
}