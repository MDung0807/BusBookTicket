using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Dapper;

namespace BusBookTicket.Buses.Repository;

public class BusTypeRepository : IBusTypeRepository
{
    public async Task<List<object>> TotalBusInType()
    {
        string query = @"SELECT BT.Id, BT.TotalSeats , COUNT(B.Id) AS Total FROM BusTypes BT
            LEFT JOIN Buses B on BT.Id = B.BusTypeID
            GROUP BY BT.Id, BT.TotalSeats";

        try
        {
            IDapperContext<object> dapperContext = new DapperContext<object>();
            var result = await dapperContext.ExecuteQueryAsync(query, new
            {});
            return result;
        }
        catch (Exception ex)
        {
            throw new ExceptionDetail(ex.Message);
        }
    }
}