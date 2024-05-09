using SubscriptionProvider.Models;

namespace SubscriptionProvider.Factories;

public class ResponseFactory
{
    public static ResponseResult Ok(object obj, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message ?? "Succeeded",
            StatusCode = StatusCode.OK,
        };
    }
    public static ResponseResult Ok(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Succeeded",
            StatusCode = StatusCode.OK,
        };
    }
    public static ResponseResult Error(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Error",
            StatusCode = StatusCode.ERROR
        };
    }
    public static ResponseResult Exists(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Exists",
            StatusCode = StatusCode.EXISTS
        };
    }
    public static ResponseResult Exists(object obj, string? message = null)
    {
        return new ResponseResult
        {
            ContentResult = obj,
            Message = message ?? "Exists",
            StatusCode = StatusCode.EXISTS
        };
    }
    public static ResponseResult NotFound(string? message = null)
    {
        return new ResponseResult
        {
            Message = message ?? "Not Found",
            StatusCode = StatusCode.NOT_FOUND
        };
    }
}
