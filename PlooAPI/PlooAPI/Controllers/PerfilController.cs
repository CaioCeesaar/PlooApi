using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Data;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerfilController(IConfiguration configuration, IMapper mapper, PlooDbContext context) : PlooApiControllerBase(configuration, mapper, context)
{
    
    [HttpGet]
    public async Task<IActionResult> GetPerfisAsync([FromQuery(Name = "id")] int? id)
    {
        return ConvertResultToHttpResult(await _businessClass.GetPerfisAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> PostPerfilAsync(PerfilModel perfilModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PostPerfilAsync(perfilModel));
    }
    
    [HttpPatch]
    public async Task<IActionResult> PatchPerfilAsync([FromQuery] int id,PerfilUpdateModel perfilModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PatchPerfilAsync(id, perfilModel));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePerfilAsync([FromQuery] int id)
    {
        return ConvertResultToHttpResult(await _businessClass.DeletePerfilAsync(id));
    }
    
}