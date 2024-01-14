using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace PlooAPI.Repositories;

public class SqlDapperRep
{
    private readonly SqlConnection _connection;
    
    public SqlDapperRep(string connectionString)
    {
        _connection = new(connectionString);
        Reconnect();
    }
    
    public async Task<IEnumerable<T>> GetQueryAsync<T> (string select) where T : class
    {
        Reconnect();
        return await _connection.QueryAsync<T>(select);
    }
    
    public async Task<IEnumerable<T>> GetQueryByIdAsync<T> (string select, DynamicParameters? parameters) where T : class
    {
        Reconnect();
        return await _connection.QueryAsync<T>(select, parameters);
    }
    
    private void Reconnect()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }
}