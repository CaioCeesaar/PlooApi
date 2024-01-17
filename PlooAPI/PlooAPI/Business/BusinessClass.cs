using AutoMapper;
using Dapper;
using PlooAPI.Data;
using PlooAPI.Models;
using PlooAPI.Repositories;
using System.Text.Json;

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
            var enumerable = usuarios.ToList();
            if (!enumerable.Any())
            {
                return new (false, "Usuário não encontrado", 404);
            }
            
            return new(true, JsonSerializer.Serialize(enumerable.ToList()), 200);
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
        
        if(!(await GetPerfisAsync(usuarioModel.PerfilId)).Success)
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
        if(!(await GetUsuariosAsync(id)).Success)
        {
            return new(false, "Usuário não encontrado", 404);
        }
        
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
        
        if(!(await GetPerfisAsync(usuarioModel.PerfilId)).Success)
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
    
    public async Task<Result> DeleteUsuarioAsync(int id)
    {
        var userResult = await GetUsuariosAsync(id);

        if(!userResult.Success)
        {
            return new Result(false, "Usuário não encontrado", 404);
        }
        var usuarioList = JsonSerializer.Deserialize<List<Usuario>>(userResult.Message);
        if (usuarioList != null)
        {
            var usuario = usuarioList.FirstOrDefault();
            if (usuario != null)
            {
                usuario.Ativo = false;

                var result = await _sqlEfCoreRep.DeleteQueryAsync(usuario);

                if (result.Success)
                {
                    return new Result(true, "Usuário excluído com sucesso", 200);
                }
            }
        }
        return new Result(false, "Erro ao excluir usuário", 500);
    }
    
    public async Task<Result> GetPerfisAsync(int? id)
    {
        if (id.HasValue)
        {
            var sql = "EXEC spListarPerfilPorId @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var perfis =  await _sqlDapperRep.GetQueryByIdAsync<Perfil>(sql, parameters);
            var enumerable = perfis.ToList();
            if (!enumerable.Any())
            {
                return new(false, "Perfil não encontrado", 404);
            }
            
            return new(true, JsonSerializer.Serialize(enumerable.ToList()), 200);
        }
        
        var sql2 = "EXEC spListarPerfis";
        var perfil =  await _sqlDapperRep.GetQueryAsync<Perfil>(sql2);
        return new(true, JsonSerializer.Serialize(perfil.ToList()), 200);
    }

    public async Task<Result> PostPerfilAsync(PerfilModel perfilModel)
    {
        if (string.IsNullOrWhiteSpace(perfilModel.Nome))
        {
            return new(false, "Nome não pode ser nulo ou vazio", 400);
        }
        
        var perfil = _mapper.Map<Perfil>(perfilModel);
        perfil.Ativo = true;
        return await _sqlEfCoreRep.PostQueryAsync(perfil);
    }
    
    public async Task<Result> PatchPerfilAsync(int id, PerfilModel perfilModel)
    {
        if(!(await GetPerfisAsync(id)).Success)
        {
            return new (false, "Perfil não encontrado", 404);
        }
        
        if (string.IsNullOrWhiteSpace(perfilModel.Nome))
        {
            return new(false, "Nome não pode ser nulo ou vazio", 400);
        }
        
        var perfil = _mapper.Map<Perfil>(perfilModel);

        return await _sqlEfCoreRep.PatchQueryAsync(perfil);
    }
    
    public async Task<Result> DeletePerfilAsync(int id)
    {
        var perfilResult = await GetPerfisAsync(id);
        
        if (!perfilResult.Success)
        {
            return new(false, "Perfil não encontrado", 404);
        }
        var perfilList = JsonSerializer.Deserialize<List<Perfil>>(perfilResult.Message);
        if (perfilList != null)
        {
            var perfil = perfilList.FirstOrDefault();
            if (perfil != null)
            {
                perfil.Ativo = false;
                var result = await _sqlEfCoreRep.DeleteQueryAsync(perfil);

                if (result.Success)
                {
                    return new Result(true, "Perfil excluído com sucesso", 200);
                }
            }

            
        }
        return new(false, "Erro ao excluir perfil", 500);
    }
    
    public async Task<Result> GetEquipesAsync(int? id)
    {
        if (id.HasValue)
        {
            var sql = "EXEC spListarEquipePorId @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var equipes = await _sqlDapperRep.GetQueryByIdAsync<Equipe>(sql, parameters);
            var enumerable = equipes.ToList();
            if (!enumerable.Any())
            {
                return new(false, "Equipe não encontrada", 404);
            }
            
            return new(true, JsonSerializer.Serialize(enumerable.ToList()), 200);
        }
        
        var sql2 = "EXEC spListarEquipes";
        var equipe = await _sqlDapperRep.GetQueryAsync<Equipe>(sql2);
        return new(true, JsonSerializer.Serialize(equipe.ToList()), 200);
    }
    
    
}