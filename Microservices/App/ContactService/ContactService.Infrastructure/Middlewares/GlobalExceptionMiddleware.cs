using System.Net;
using System.Text.Json;
using ContactService.Contracts.Response;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ContactService.Infrastructure.Middleware;

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
        catch (RpcException grpcEx)
        {
            _logger.LogError(grpcEx, "gRPC error: {Message}", grpcEx.Status.Detail);
            context.Response.StatusCode = grpcEx.StatusCode == StatusCode.NotFound ? 404 : 500;
            await context.Response.WriteAsJsonAsync(new
            {
                error = grpcEx.Status.Detail,
                code = grpcEx.StatusCode.ToString()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var result = Result<string>.Error("An unexpected error occurred", 500);

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}