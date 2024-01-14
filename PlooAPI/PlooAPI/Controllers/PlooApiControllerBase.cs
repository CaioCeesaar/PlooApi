using Microsoft.AspNetCore.Mvc;
using PlooAPI.Business;

namespace PlooAPI.Controllers;

public class PlooApiControllerBase : ControllerBase
{
    protected BusinessClass _businessClass;
    
    protected IActionResult ConvertResultToHttpResult(Result result)
    {
        switch (result.StatusCode)
        {
            case 200:
                return Ok(result.Message);
            case 201:
                return Created("", result.Message);
            case 204:
                return NoContent();
            case 400:
                return BadRequest(result.Message);
            case 404:
                return NotFound(result.Message);
            default:
                return StatusCode(result.StatusCode, result.Message);
        }
    }
}