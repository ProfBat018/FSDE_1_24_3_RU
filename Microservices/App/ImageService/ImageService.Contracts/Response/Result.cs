namespace ImageService.Contracts.Response;

public class Result<T>
{
    public Result(bool isSuccess, T? data, string? message = null, int? errorCode = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        ErrorCode = errorCode;
    }

    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public int? ErrorCode { get; init; }
    public T? Data { get; init; }

    public static Result<T> Success(T data, string? message = null) =>
        new(true, data, message);

    public static Result<T> Error(string message, int errorCode = 500, T? data = default) =>
        new(false, data, message, errorCode);
}