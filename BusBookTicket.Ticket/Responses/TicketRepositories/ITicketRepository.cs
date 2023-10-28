using BusBookTicket.Core.Common;
using BusBookTicket.Ticket.DTOs.Response;

namespace BusBookTicket.Ticket.Responses.TicketRepositories;

public interface ITicketRepository : IRepository<Core.Models.Entity.Ticket, int>
{
    Task<List<Core.Models.Entity.Ticket>> getAllTicket(DateTime dateTime, string stationStart, string stationEnd);
}