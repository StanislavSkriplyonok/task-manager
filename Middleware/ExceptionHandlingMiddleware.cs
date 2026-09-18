using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Exceptions;

namespace TaskManager.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");

            if (context.Response.HasStarted)
            {
                throw;
            }

            await HandleExceptionAsync(context, ex);

        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            BadRequestException => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = statusCode == HttpStatusCode.InternalServerError
            ? "An unexpected error occurred"
            : exception.GetType().Name,
            Detail = statusCode == HttpStatusCode.InternalServerError
            ? "Please try again later."
            : exception.Message,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(problemDetails);
        return context.Response.WriteAsync(json);
    }
}