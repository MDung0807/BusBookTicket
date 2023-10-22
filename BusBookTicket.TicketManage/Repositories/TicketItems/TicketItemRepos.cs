using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.TicketManage.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.TicketManage.Repositories.TicketItems;

public class TicketItemRepos : ITicketItemRepos
{
    private readonly AppDBContext _context;

    public TicketItemRepos(AppDBContext context)
    {
        this._context = context;
    }

    public async Task<List<TicketItem>> getAllItems(int ticketID)
    {
        try
        {
            return await _context.TicketItems.Where(x => 
                x.ticket != null && x.ticket.ticketID == ticketID)
                .ToListAsync();
        }
        catch 
        {
            throw new Exception(TicketConstants.ERROR_GET);
        }
    }
    public Task<TicketItem> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> update(TicketItem entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> delete(TicketItem entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketItem>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(TicketItem entity)
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