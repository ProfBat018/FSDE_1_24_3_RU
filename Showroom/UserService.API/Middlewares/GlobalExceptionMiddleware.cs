using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UserService.API.DTOs.Response;

namespace UserService.API.Middlewares;

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

        TypedResult<string> errorRes;

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = 200;
                errorRes = TypedResult<string>.Error($"Validation failed: {validationEx.Message}", 400);
                break;

            case AutoMapperMappingException mapEx:
                context.Response.StatusCode = 200;
                errorRes = TypedResult<string>.Error("Ошибка маппинга: проверьте DTO и конфигурацию AutoMapper.", 400);
                break;
            case InvalidCredentialException authEx:
                context.Response.StatusCode = 200;
                errorRes = TypedResult<string>.Error($"Authentication failed: {authEx.Message}", 401);
                break;
            default:
                errorRes = TypedResult<string>.Error(exception.Message);
                context.Response.StatusCode = 400;
                break;
        }

        var jsonSerializerSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        
        var result = JsonConvert.SerializeObject(errorRes, jsonSerializerSettings);
        
        await context.Response.WriteAsync(result);
    }
}