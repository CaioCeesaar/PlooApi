using System.Data;
using System.Data.SqlClient;
using Dapper;
using PlooAPI.Business;

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

    public async Task<Result> PostQueryAsync(string insert)
    {
        try
        {
            Reconnect();
            var ins = await _connection.ExecuteAsync(insert);
      
            return new(true, "Ok");
        }
        catch (Exception ex)
        {
            return new(false, "Deu ruim");
        }
    }
    
    private void Reconnect()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }
}