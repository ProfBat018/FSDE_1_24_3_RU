namespace AuditService.Contracts.Response;

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? Message { get; init; }
    public string? ErrorCode { get; init; }

    public static Result<T> Success(T data, string? message = null) =>
        new() { IsSuccess = true, Data = data, Message = message };

    public static Result<T> Failure(string message, string? errorCode = null) =>
        new() { IsSuccess = false, Message = message, ErrorCode = errorCode };
}