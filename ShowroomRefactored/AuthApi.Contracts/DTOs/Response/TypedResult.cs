namespace AuthApi.Contracts.DTOs.Response;

public class TypedResult<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public int StatusCode { get; }
    public T? Data { get; }

    private TypedResult(bool isSuccess, string message, int statusCode, T? data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        Data = data;
    }

    public static TypedResult<T> Success(T? data = default, string message = "Success", int statusCode = 200)
    {
        return new TypedResult<T>(true, message, statusCode, data);
    }

    public static TypedResult<T> Error(string message = "An error occurred", int statusCode = 400)
    {
        return new TypedResult<T>(false, message, statusCode, default);
    }
}
