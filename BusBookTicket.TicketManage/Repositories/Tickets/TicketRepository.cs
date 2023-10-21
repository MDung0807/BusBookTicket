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
    public async Task<Ticket> getByID(int id)
    {
        try
        {
            return await _context.Tickets.Where(x => x.ticketID == id)
                .Include(x => x.ticketItems)
                .Include(x => x.customer)
                .Include(x => x.discount)
                .Include(x => x.busStationEnd)
                .Include(x => x.busStationStart).FirstAsync();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }

    public Task<int> update(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> delete(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ticket>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(Ticket entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR_CREATE);
        }
    }
    
    
}