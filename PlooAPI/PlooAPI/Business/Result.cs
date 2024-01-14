namespace PlooAPI.Business;

public class Result
{
    public Result(bool success, string message, int statusCode)
    {
        Sucess = success;
        Message = message;
        StatusCode = statusCode;
    }
    
    public bool Sucess { get; set; }
    
    public string Message { get; set; }
    
    public int StatusCode { get; set; }
}