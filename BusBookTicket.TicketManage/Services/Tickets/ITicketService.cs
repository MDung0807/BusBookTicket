using BusBookTicket.Core.Common;
using BusBookTicket.TicketManage.DTOs.Requests;
using BusBookTicket.TicketManage.DTOs.Responses;

namespace BusBookTicket.TicketManage.Services.Tickets;

public interface ITicketService: IService<TicketRequest, TicketRequest, int, TicketResponse>
{
    
}