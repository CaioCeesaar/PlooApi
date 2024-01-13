namespace PlooAPI.Business;

public class Result
{
    public Result(bool success, string message)
    {
        Sucess = success;
        Message = message;
    }
    
    public bool Sucess { get; set; }
    
    public string Message { get; set; }
}