namespace BenzanFF.Data;

public class Result
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    //Metodos para mejorar las respuestas...
    public static Result Successed(string message = "Ok") => new() { Success = true, Message = message };
    public static Result Failed(string message ) => new() { Success = false, Message = message };

}
public class Result<T>:Result
{
    public T? Data { get; set; }

    public static Result<T> Successed(T? data, string message = "Ok") => new() { Data = data, Success = true, Message = message };
    public static new Result<T> Failed(string message) => new() { Data = default(T), Success = false, Message = message };
}
