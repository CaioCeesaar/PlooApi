namespace PlooAPI.Business;

public class Result
{
    public Result(bool success, string message, int statusCode)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }
    
    public bool Success { get; set; }
    
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}