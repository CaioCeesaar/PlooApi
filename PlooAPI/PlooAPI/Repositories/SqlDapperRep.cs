using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace PlooAPI.Repositories;

public class SqlDapperRep
{
    private readonly SqlConnection _connection;
    
    public SqlDapperRep(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
        Reconnect();
    }
    
    public async Task<IEnumerable<T>> GetQueryAsync<T> (string select) where T : class
    {
        Reconnect();
        return await _connection.QueryAsync<T>(select);
    }
    
    private void Reconnect()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }
}