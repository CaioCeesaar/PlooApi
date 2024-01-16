using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlooAPI.Data;
using PlooAPI.Models;

namespace PlooAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipeController(IConfiguration configuration, IMapper mapper, PlooDbContext context) : PlooApiControllerBase(configuration, mapper, context)
{
    // TODO: Implementar métodos de EquipeController
    // TODO: Relação Many to Many entre Equipe e Usuario
    /*[HttpGet]
    public async Task<IActionResult> GetEquipesAsync([FromQuery(Name = "id")] int? id)
    {
        return ConvertResultToHttpResult(await _businessClass.GetEquipesAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> PostEquipeAsync(EquipeModel equipeModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PostEquipeAsync(equipeModel));
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchEquipeAsync([FromQuery] int id,EquipeModel equipeModel)
    {
        return ConvertResultToHttpResult(await _businessClass.PatchEquipeAsync(id, equipeModel));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEquipeAsync([FromQuery] int id)
    {
        return ConvertResultToHttpResult(await _businessClass.DeleteEquipeAsync(id));
    }*/
}