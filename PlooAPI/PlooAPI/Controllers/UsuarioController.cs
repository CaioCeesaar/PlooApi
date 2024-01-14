using Microsoft.AspNetCore.Mvc;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : PlooApiControllerBase
{
    public UsuarioController(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        _businessClass = new(connectionString);
    }

    [HttpGet]
    public async Task<IEnumerable<Usuario>> GetUsuarioByIdAsync([FromQuery(Name = "id")] int id)
    {
        var getJogador = await _businessClass.GetUsuarioByIdAsync(id);
        return getJogador;
    }
    
}