using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Data;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController(IConfiguration configuration, IMapper mapper, PlooDbContext context) : PlooApiControllerBase(configuration, mapper, context)
{
    [HttpGet]
    public async Task<IActionResult> GetUsuariosAsync([FromQuery(Name = "id")] int? id)
    {
        return ConvertResultToHttpResult(await _businessClass.GetUsuariosAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostUsuarioAsync(UsuarioModel usuarioModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PostUsuarioAsync(usuarioModel));
    } 
    
    [HttpPatch]
    public async Task<IActionResult> PatchUsuarioAsync(UsuarioModel usuarioModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PatchUsuarioAsync(usuarioModel));
    }
    
}