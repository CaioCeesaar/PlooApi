using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Business;
using PlooAPI.Data;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : PlooApiControllerBase
{
    public UsuarioController(IConfiguration configuration, IMapper mapper, PlooDbContext context)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _businessClass = new BusinessClass(context, connectionString, mapper);
    }
    
    [HttpGet]
    public async Task<IEnumerable<Usuario>> GetUsuariosAsync([FromQuery(Name = "id")] int? id)
    {
        var getUsuario = await _businessClass.GetUsuariosAsync(id);
        return getUsuario;
    }

    [HttpPost]
    public async Task<Result> PostUsuarioAsync(UsuarioModel usuarioModel)
    {
        await _businessClass.PostUsuarioAsync(usuarioModel);
        return new(true, "Usuario inserido com sucesso", 201);
    } 
    
}