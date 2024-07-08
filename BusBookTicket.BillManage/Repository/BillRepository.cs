using BusBookTicket.Core.Infrastructure.Dapper;

namespace BusBookTicket.BillManage.Repository;

public class BillRepository : IBillRepository
{
    public async Task<List<object>> StatisticalInMonth(int idMaster, int month, int year)
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
            COALESCE(CAST(SUM(b.TotalPrice) AS decimal)/100000000, 0) AS TotalPrice
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
        
		IDapperContext<object> dapperContext = new DapperContext<object>();
        var result = await dapperContext.ExecuteQueryAsync(query: query, 
            new
            {
                month = month, 
                year = year,
                companyId = idMaster
            });
        return result;
    }

    public async Task<decimal> Sales(int idMaster, int month, int year)
    {
	    try
	    {
		    string query = @"
            SELECT COALESCE(SUM(TotalPrice), 0) as TotalPrice
            FROM (
                SELECT B.DateDeparture, B.TotalPrice 
                FROM Bills B
                INNER JOIN TicketRouteDetails TR ON TR.Id = B.TicketRouteDetailEndId
                INNER JOIN Tickets T ON T.Id = TR.TicketId
                INNER JOIN Buses BUS ON BUS.Id = T.BusID
                WHERE MONTH(B.DateDeparture) = @month
                AND YEAR(B.DateDeparture) = @year
                AND BUS.CompanyID = @companyId
            ) AS SUB"; 

		    // Assuming DapperContext is implemented correctly
		    IDapperContext<decimal> dapperContext = new DapperContext<decimal>();
        
		    // Execute the query with the parameters
		    var result = await dapperContext.ExecuteQueryAsync(query: query, 
			    new
			    {
				    month = month, 
				    year = year,
				    companyId = idMaster
			    });

		    // Return the result or 0 if null or empty
		    return result == null ? 0 : result.FirstOrDefault();
	    }
	    catch (Exception ex)
	    {
		    // Log the exception (using your preferred logging framework)
		    // e.g., Console.WriteLine(ex.Message);
		    throw new Exception("An error occurred while fetching sales data.", ex);
	    }
    }


    public async Task<int> TotalBillInMonth(int idMaster, int month, int year)
    {
	    string query = @"
			select COUNT(BillId) as TotalBill FROM (
				SELECT BillId FROM 
			        (SELECT Id as BillId, TicketRouteDetailEndId FROM Bills
				        WHERE MONTH(DateDeparture) = @month
				        AND YEAR(DateDeparture) = @year) B
			        INNER JOIN TicketRouteDetails TR ON TR.Id = b.TicketRouteDetailEndId
			        INNER JOIN Tickets T ON T.Id = TR.TicketId
			        INNER JOIN Buses BUS ON BUS.Id = T.BusID
			        INNER JOIN (
				        SELECT TOP 1 Id FROM Companies WHERE Id = @companyId
			        ) AS CO ON CO.Id = BUS.CompanyID

			) AS SUB";
	    
	    IDapperContext<int> dapperContext = new DapperContext<int>();
	    var result = await dapperContext.ExecuteQueryAsync(query: query, 
		    new
		    {
			    month = month, 
			    year = year,
			    companyId = idMaster
		    });
	    return result.ToList().FirstOrDefault();
    }

    public async Task<List<object>> TopRouteInBill(int companyId, int top)
    {
	    int year = DateTime.Now.Year;
	    string query = @"
			SELECT TOP (@top) COUNT(*) AS TOTAL ,BSStart.Name, BSEnd.Name FROM (
			SELECT TicketRouteDetailStartId, id FROM Bills) AS B
			INNER JOIN BillItems BI ON BI.BillID = B.Id
			INNER JOIN TicketRouteDetails TR ON TR.Id = b.TicketRouteDetailStartId
			INNER JOIN Tickets T ON T.Id = TR.TicketId
			INNER JOIN Buses BUS ON BUS.Id = T.BusID
			INNER JOIN (
				SELECT TOP 1 Id FROM Companies WHERE Id = 1
			) AS CO ON CO.Id = BUS.CompanyID
			INNER JOIN RouteDetails RD ON RD.Id = TR.RouteDetailId
			INNER JOIN Routes R ON R.Id = RD.RouteId
			INNER JOIN BusStations BSStart ON BSStart.Id = R.StationStartId
			INNER JOIN BusStations BSEnd ON BSEnd.Id = R.StationEndId

			GROUP BY R.Id, BSStart.Name, BSEnd.Name
			ORDER BY TOTAL DESC";
	    IDapperContext<object> dapperContext = new DapperContext<object>();
	    var result = await dapperContext.ExecuteQueryAsync(query: query, 
		    new
		    {
			    top = top, 
			    year = year,
			    companyId = companyId
		    });
	    return result;
    }
    
}