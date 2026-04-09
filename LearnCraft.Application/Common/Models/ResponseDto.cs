using System.Text.Json.Serialization;

namespace LearnCraft.Application.Common.Models;

public class ResponseDto<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }

    [JsonConstructor]
    public ResponseDto() { }

    private ResponseDto(int statusCode, string message, T? data, bool isSuccess)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
        IsSuccess = isSuccess;
    }

    public static ResponseDto<T> Success(T data, string message = "Success", int statusCode = 200)
    {
        return new ResponseDto<T>(statusCode, message, data, true);
    }

    public static ResponseDto<T> Failure(string message, int statusCode = 400)
    {
        return new ResponseDto<T>(statusCode, message, default, false);
    }
}
