using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Business;
using PlooAPI.Data;

namespace PlooAPI.Controllers;

public class PlooApiControllerBase : ControllerBase
{

    protected BusinessClass _businessClass;

    public PlooApiControllerBase(IConfiguration configuration, IMapper mapper, PlooDbContext context)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        _businessClass = new(context, connectionString, mapper);
    }

    protected IActionResult ConvertResultToHttpResult(Result result)
    {
        return result.StatusCode switch
        {
            200 => Ok(result.Message),
            201 => Created("", result.Message),
            204 => NoContent(),
            400 => BadRequest(result.Message),
            404 => NotFound(result.Message),
            _ => StatusCode(result.StatusCode, result.Message),
        };
    }
}