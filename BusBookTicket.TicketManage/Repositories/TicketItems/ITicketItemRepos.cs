using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;

namespace BusBookTicket.TicketManage.Repositories.TicketItems;

public interface ITicketItemRepos : IRepository<TicketItem, int>
{
    Task<List<TicketItem>> getAllItems(int ticketID);
}