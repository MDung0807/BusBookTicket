using BusBookTicket.Core.Common;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Migrations;
using BusBookTicket.Ticket.DTOs.Requests;
using BusBookTicket.Ticket.DTOs.Response;

namespace BusBookTicket.Ticket.Services.TicketItemServices;

public interface ITicketItemService : IService<TicketItemForm, TicketItemForm, int, TicketItemResponse, object, object>
{
    Task<List<TicketItemResponse>> GetAllInTicket(int ticketId);
    Task<bool> ChangeStatusToWaitingPayment(int id, int userId);
    Task<bool> ChangeStatusToPaymentComplete(int id, int userId);
}