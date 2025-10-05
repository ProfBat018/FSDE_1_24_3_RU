
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using ImageService.Contracts.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ImageService.Infrastructure.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }


        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            var response = context.Response;
            response.ContentType = "application/json";

            var (statusCode, result) = ex switch
            {
                DbUpdateException => (
                    HttpStatusCode.Conflict,
                    Result<string>.Error("A database error occurred", 409)
                ),

                UnauthorizedAccessException => (
                    HttpStatusCode.Unauthorized,
                    Result<string>.Error("Unauthorized access", 401)
                ),

                _ => (
                    HttpStatusCode.InternalServerError,
                    Result<string>.Error("An unexpected error occurred", 500)
                )
            };

            response.StatusCode = (int)statusCode;

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await response.WriteAsync(json);
        }
    }
}
