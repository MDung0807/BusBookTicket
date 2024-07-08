using BusBookTicket.Core.Infrastructure.Dapper;

namespace BusBookTicket.Ticket.Repositories;

public class TicketRepository : ITicketRepository
{
    public async Task<double> TotalTicketInMonth(int companyId, int month, int year)
    {
        string query = @"
            SELECT COUNT(*) FROM (
            SELECT id, BusID FROM Tickets 
            WHERE MONTH(DATE) = @month AND YEAR(DATE) = @year) AS T
                INNER JOIN TicketItems TI ON TI.TicketID = T.Id
	            INNER JOIN Buses BUS ON BUS.Id = T.BusID
						INNER JOIN (
							SELECT TOP 1 Id FROM Companies WHERE Id = @companyId
						) AS CO ON CO.Id = BUS.CompanyID";
        IDapperContext<int> dapperContext = new DapperContext<int>();
        var result = await dapperContext.ExecuteQueryAsync(query: query, 
            new
            {
                month = month, 
                year = year,
                companyId = companyId
            });
        return result == null ? 0: result.ToList().FirstOrDefault();
    }
}