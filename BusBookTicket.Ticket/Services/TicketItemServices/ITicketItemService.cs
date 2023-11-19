using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;

namespace BusBookTicket.Ticket.Services.TicketItemServices;

public interface ITicketItemService : IService<TicketItemForm, TicketItemForm, int, TicketItemResponse>
{
    Task<List<TicketItemResponse>> getAllInTicket(int ticketId);
    Task<bool> ChangeStatusToWaitingPayment(int id, int userId);
    Task<bool> ChangeStatusToPaymentComplete(int id, int userId);
}