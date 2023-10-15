using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;

namespace BusBookTicket.TicketManage.Repositories.TicketItems;

public interface ITicketItemRepos : IRepository<TicketItem, int>
{
    List<TicketItem> getAllItems(int ticketID);
}