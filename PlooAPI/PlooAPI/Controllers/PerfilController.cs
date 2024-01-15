using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Business;
using PlooAPI.Data;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerfilController : PlooApiControllerBase
{
    public PerfilController(IConfiguration configuration, IMapper mapper, PlooDbContext context)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _businessClass = new BusinessClass(context, connectionString, mapper);
    }
    
    [HttpGet]
    public async Task<IEnumerable<Perfil>> GetPerfisAsync([FromQuery(Name = "id")] int? id)
    {
        var getPerfil = await _businessClass.GetPerfisAsync(id);
        return getPerfil;
    }
    
    [HttpPost]
    public async Task<Result> PostPerfilAsync(PerfilModel perfilModel)
    {
        await _businessClass.PostPerfilAsync(perfilModel);
        return new(true, "Perfil inserido com sucesso", 201);
    }
}