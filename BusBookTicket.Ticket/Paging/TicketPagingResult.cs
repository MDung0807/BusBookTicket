using BusBookTicket.Core.Application.Paging;
using BusBookTicket.Ticket.DTOs.Response;

namespace BusBookTicket.Ticket.Paging;

public class TicketPagingResult : PagingResult<TicketResponse>
{
    public TicketPagingResult(int take, int size, int totalPage, List<TicketResponse> items)
    {
        PageIndex = take;
        PageSize = size;
        PageTotal = totalPage;
        Items = items;
    }

    public TicketPagingResult(){}
}