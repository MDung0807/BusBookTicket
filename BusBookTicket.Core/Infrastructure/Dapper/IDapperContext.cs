namespace BusBookTicket.Core.Infrastructure.Dapper;

public interface IDapperContext <T>
{
    /// <summary>
    /// Execute query as async
    /// </summary>
    /// <param name="query"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<List<T>> ExecuteQueryAsync(string query, object parameters = null);
}