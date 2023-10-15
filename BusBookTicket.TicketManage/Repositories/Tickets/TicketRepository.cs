using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.TicketManage.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.TicketManage.Repositories.Tickets;

public class TicketRepository : ITicketRepository
{
    private readonly AppDBContext _context;

    public TicketRepository(AppDBContext context)
    {
        this._context = context;
    }
    public Ticket getByID(int id)
    {
        try
        {
            return _context.Tickets.Where(x => x.ticketID == id)
                .Include(x => x.ticketItems)
                .Include(x => x.customer)
                .Include(x => x.discount)
                .Include(x => x.busStationEnd)
                .Include(x => x.busStationStart).First();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }

    public int update(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public int delete(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public List<Ticket> getAll()
    {
        throw new NotImplementedException();
    }

    public int create(Ticket entity)
    {
        int id;
        try
        {
            id = _context.Add(entity).Entity.ticketID;
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_CREATE);
        }

        return id;
    }
    
    
}