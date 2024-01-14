using Dapper;
using PlooAPI.Data;
using PlooAPI.Models;
using PlooAPI.Repositories;

namespace PlooAPI.Business;

public class BusinessClass
{
    private readonly SqlDapperRep _sqlDapperRep;
    private readonly SqlEfCoreRep _sqlEfCoreRep;
    private readonly ApiRep _apiRep;
    
    public BusinessClass(PlooDbContext context, string connectionString)
    {
        _sqlDapperRep = new(connectionString);
        _sqlEfCoreRep = new(context);
        _apiRep = new();
    }

    public BusinessClass(string connectionString)
    {
        _sqlDapperRep = new(connectionString);
    }

    public async Task<IEnumerable<Usuario>> GetUsuarioAsync()
    {
        var sql = "EXEC spListarUsuarioAtivoPerfilEquipe";
        return await _sqlDapperRep.GetQueryAsync<Usuario>(sql);
    }
    
    public async Task<IEnumerable<Usuario>> GetUsuarioByIdAsync(int id)
    {
        var sql = $@"EXEC spListarUsuarioAtivoPerfilEquipePorId @idUsuario";
        var parameters = new DynamicParameters();
        parameters.Add("@idUsuario", id);
        
        return await _sqlDapperRep.GetQueryByIdAsync<Usuario>(sql, parameters);
    }
    
    public async Task<IEnumerable<T>> GetEntityAsync<T>() where T : class
    {
        var entityType = typeof(T);
        var sql = $@"SELECT * FROM {entityType.Name} ";
        return await _sqlDapperRep.GetQueryAsync<T>(sql);
    }
}