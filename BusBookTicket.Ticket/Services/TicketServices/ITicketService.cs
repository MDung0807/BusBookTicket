using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Paging;

namespace BusBookTicket.Ticket.Services.TicketServices;

public interface ITicketService : IService<TicketFormCreate, TicketFormUpdate, int, TicketResponse, TicketPaging, TicketPagingResult>
{
    /// <summary>
    /// FindTicket
    /// </summary>
    /// <param name="searchForm"></param>
    /// <param name="paging"></param>
    /// <returns></returns>
    Task<TicketPagingResult> GetAllTicket(SearchForm searchForm, TicketPaging paging);
}