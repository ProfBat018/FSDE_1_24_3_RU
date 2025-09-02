using System.ComponentModel.DataAnnotations;
using AuthApi.Contracts.DTOs.Response;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AuthApi.Infrastructure.Middlewares;


public class GlobalExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400;

        TypedResult<string> errorRes;

        switch (exception)
        {
            case ValidationException validationEx:
                errorRes = TypedResult<string>.Error($"Validation failed: {validationEx.Message}");
                break;

            case AutoMapperMappingException mapEx:
                errorRes = TypedResult<string>.Error("Ошибка маппинга: проверьте DTO и конфигурацию AutoMapper.");
                break;

            default:
                errorRes = TypedResult<string>.Error(exception.Message);
                break;
        }

        var result = JsonConvert.SerializeObject(errorRes);
        await context.Response.WriteAsync(result);
    }
}