using Microsoft.AspNetCore.Mvc;
using PlooAPI.Business;

namespace PlooAPI.Controllers;

public class PlooApiControllerBase : ControllerBase
{
    protected BusinessClass BusinesssClass = new();
    
    protected IActionResult ConvertResultToHttpResult(Result result)
    {
        if (result.Sucess)
        {
            return Ok(result.Message);
        }
        
        return BadRequest(result.Message);
    }
}