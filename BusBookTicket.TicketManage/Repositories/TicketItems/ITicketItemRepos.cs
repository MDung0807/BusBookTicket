using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.TicketManage.Repositories.TicketItems;

public interface ITicketItemRepos : IRepository<TicketItem, int>
{
    Task<List<TicketItem>> getAllItems(int ticketID);
}