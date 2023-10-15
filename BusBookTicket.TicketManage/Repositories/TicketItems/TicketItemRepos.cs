using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.TicketManage.Utilities;

namespace BusBookTicket.TicketManage.Repositories.TicketItems;

public class TicketItemRepos : ITicketItemRepos
{
    private readonly AppDBContext _context;

    public TicketItemRepos(AppDBContext context)
    {
        this._context = context;
    }

    public List<TicketItem> getAllItems(int ticketID)
    {
        try
        {
            return _context.TicketItems.Where(x => x.ticket.ticketID == ticketID).ToList();
        }
        catch 
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }
    public TicketItem getByID(int id)
    {
        throw new NotImplementedException();
    }

    public int update(TicketItem entity)
    {
        throw new NotImplementedException();
    }

    public int delete(TicketItem entity)
    {
        throw new NotImplementedException();
    }

    public List<TicketItem> getAll()
    {
        throw new NotImplementedException();
    }

    public int create(TicketItem entity)
    {
        int id;
        try
        {
            id = _context.Add(entity).Entity.ticketItemID;
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_CREATE);
        }

        return id;
    }
}