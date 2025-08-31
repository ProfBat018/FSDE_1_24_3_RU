namespace Ecommerce.Shared.DTOs.Response;


public class Result
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public int StatusCode { get; }

    private Result(bool isSuccess, string message, int statusCode)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
    }

    public static Result Success(string message = "Success", int statusCode = 200)
    {
        return new Result(true, message, statusCode);
    }

    public static Result Error(string message = "An error occurred", int statusCode = 400)
    {
        return new Result(false, message, statusCode);
    }
}