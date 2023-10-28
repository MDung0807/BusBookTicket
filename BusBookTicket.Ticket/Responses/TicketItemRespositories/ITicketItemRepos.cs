using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Ticket.Responses.TicketItemRespositories;

public interface ITicketItemRepos : IRepository<TicketItem, int>
{
    Task<List<TicketItem>> getAllItem(int ticketID);
}