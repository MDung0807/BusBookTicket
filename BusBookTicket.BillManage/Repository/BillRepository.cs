using BusBookTicket.Core.Infrastructure.Dapper;

namespace BusBookTicket.BillManage.Repository;

public class BillRepository : IBillRepository
{
    private readonly IDapperContext<object> _dapperContext;

    public BillRepository(IDapperContext<object> dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<object>> Statistical(int idMaster, int month, int year)
    {
        string query = @"
        WITH Calendar AS (
        SELECT 1 AS Day
        UNION ALL
        SELECT Day + 1
        FROM Calendar
        WHERE Day < 31
        )
        SELECT 
            c.Day,
            COALESCE(SUM(b.TotalPrice), 0) AS TotalPrice
        FROM 
            Calendar c
            LEFT JOIN (
		        SELECT DateDeparture, TotalPrice FROM 
			        (SELECT * FROM Bills
				        WHERE MONTH(DateDeparture) = @month
				        AND YEAR(DateDeparture) = @year) B
			        INNER JOIN TicketRouteDetails TR ON TR.Id = b.TicketRouteDetailEndId
			        INNER JOIN Tickets T ON T.Id = TR.TicketId
			        INNER JOIN Buses BUS ON BUS.Id = T.BusID
			        INNER JOIN (
				        SELECT TOP 1 Id FROM Companies WHERE Id = @companyId
			        ) AS CO ON CO.Id = BUS.CompanyID
	        ) AS B
	        ON DAY(B.DateDeparture) = c.Day
        GROUP BY 
            c.Day
        ORDER BY 
            c.Day
        OPTION (MAXRECURSION 0)";
        var result = await _dapperContext.ExecuteQueryAsync(query: query, 
            new
            {
                @month = month, 
                @year = year,
                @companyId = idMaster
            });
        return result;
    }
}