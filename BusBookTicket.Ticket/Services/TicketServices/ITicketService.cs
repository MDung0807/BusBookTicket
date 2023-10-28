using BusBookTicket.Core.Common;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;

namespace BusBookTicket.Ticket.Services.TicketServices;

public interface ITicketService : IService<TicketFormCreate, TicketFormUpdate, int, TicketResponse>
{
    Task<List<TicketResponse>> getAllTicket(DateTime dateTime, string stationStart, string stationEnd);
}