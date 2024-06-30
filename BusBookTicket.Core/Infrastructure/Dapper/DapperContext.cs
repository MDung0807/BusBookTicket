using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BusBookTicket.Core.Infrastructure.Dapper;

public class DapperContext<T> : IDapperContext<T>
{
    private readonly string _connectionString;
    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:DefaultDB"];
    }

    public async Task<List<T>> ExecuteQueryAsync(string query, object parameters)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var result = await connection.QueryAsync<T>(query, param: parameters);
        return result.ToList();
    }
}