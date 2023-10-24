using BusBookTicket.Core.Common;
using BusBookTicket.TicketManage.DTOs.Requests;
using BusBookTicket.TicketManage.DTOs.Responses;

namespace BusBookTicket.TicketManage.Services.TicketItems;

public interface ITicketItemService : IService<TicketItemRequest, TicketItemRequest, int, TicketItemResponse>
{
    /// <summary>
    /// Get all item in ticket
    /// </summary>
    /// <param name="ticketID">ID bill</param>
    /// <returns>List all item in ticket</returns>
    Task<List<TicketItemResponse>> GetItemInTicket(int ticketID);
}