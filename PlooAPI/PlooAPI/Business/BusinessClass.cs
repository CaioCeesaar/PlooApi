using AutoMapper;
using Dapper;
using PlooAPI.Data;
using PlooAPI.Models;
using PlooAPI.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlooAPI.Business;

public class BusinessClass(PlooDbContext context, string connectionString, IMapper mapper)
{
    private readonly SqlDapperRep _sqlDapperRep = new(connectionString);
    private readonly SqlEfCoreRep _sqlEfCoreRep = new(context);
    private readonly IMapper _mapper = mapper;
    private readonly ApiRep _apiRep = new();

    public async Task<Result> GetUsuariosAsync(int? id)
    {
        if (id.HasValue)
        {
            var sql = "EXEC spListarUsuarioAtivoPerfilEquipePorId @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var usuarios = await _sqlDapperRep.GetQueryByIdAsync<Usuario>(sql, parameters);
            return new(true, JsonSerializer.Serialize(usuarios.ToList()), 200);
        }
        
        var sql2 = "EXEC spListarUsuariosAtivosPerfilEquipe";
        var usuario = await _sqlDapperRep.GetQueryAsync<Usuario>(sql2);
        return new (true, JsonSerializer.Serialize(usuario.ToList()), 200);  
    }

    public async Task<Result> PostUsuarioAsync(UsuarioModel usuarioModel)
    {
        
        if (string.IsNullOrWhiteSpace(usuarioModel.Nome))
        {
            return new (false, "Nome não pode ser nulo ou vazio", 400);
        }
        
        if (usuarioModel.DataNascimento > DateTime.Now || usuarioModel.DataNascimento < DateTime.Now.AddYears(-150))
        {
            return new (false, "Data de nascimento inválida", 400);
        }
        
        if (string.IsNullOrWhiteSpace(usuarioModel.Cep))
        {
            return new (false, "Cep não pode ser nulo ou vazio", 400);
        }
        
        if(!(await GetPerfisAsync(usuarioModel.PerfilId)).Any())
        {
            return new (false, "Perfil não encontrado", 404);
        }
        
        var usuario = _mapper.Map<Usuario>(usuarioModel);
        
        var endereco = await _apiRep.ConsultarCep(usuario.Cep);
        if (endereco is not null)
        {
            usuario.Logradouro = endereco.Logradouro;
            usuario.Uf = endereco.Uf;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.Ibge = endereco.Ibge;
            usuario.Gia = endereco.Gia;
            usuario.Ddd = endereco.Ddd;
            usuario.Siafi = endereco.Siafi;

            usuario.Ativo = true;
            return await _sqlEfCoreRep.PostQueryAsync(usuario);

        }
        
        return new(false, "Cep não encontrado", 404);
    }

    public async Task<Result> PatchUsuarioAsync(int id, UsuarioModel usuarioModel)
    {
        
        if (string.IsNullOrWhiteSpace(usuarioModel.Nome))
        {
            return new(false, "Nome não pode ser nulo ou vazio", 400);
        }
        
        if (usuarioModel.DataNascimento > DateTime.Now || usuarioModel.DataNascimento < DateTime.Now.AddYears(-150))
        {
            return new (false, "Data de nascimento inválida", 400);
        }
        
        if (string.IsNullOrWhiteSpace(usuarioModel.Cep))
        {
            return new (false, "Cep não pode ser nulo ou vazio", 400);
        }
        
        if(!(await GetPerfisAsync(usuarioModel.PerfilId)).Any())
        {
            return new (false, "Perfil não encontrado", 404);
        }
        
        var usuario = _mapper.Map<Usuario>(usuarioModel);
        
        var endereco = await _apiRep.ConsultarCep(usuario.Cep);
        
        if (endereco is not null)
        {
            usuario.Logradouro = endereco.Logradouro;
            usuario.Uf = endereco.Uf;
            usuario.Bairro = endereco.Bairro;
            usuario.Localidade = endereco.Localidade;
            usuario.Ibge = endereco.Ibge;
            usuario.Gia = endereco.Gia;
            usuario.Ddd = endereco.Ddd;
            usuario.Siafi = endereco.Siafi;
            
            return await _sqlEfCoreRep.PatchQueryAsync(usuario);
        }
        return new(false, "Cep não encontrado", 404);
    }
    
    public async Task<IEnumerable<Perfil>> GetPerfisAsync(int? id)
    {
        if (id.HasValue)
        {
            var sql = "EXEC spListarPerfilPorId @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            return await _sqlDapperRep.GetQueryByIdAsync<Perfil>(sql, parameters);
        }
        
        var sql2 = "EXEC spListarPerfis";
        return await _sqlDapperRep.GetQueryAsync<Perfil>(sql2);
    }

    public async Task<Result> PostPerfilAsync(PerfilModel perfilModel)
    {
        var perfil = _mapper.Map<Perfil>(perfilModel);
        perfil.Ativo = true;
        return await _sqlEfCoreRep.PostQueryAsync(perfil);
    }
    
}