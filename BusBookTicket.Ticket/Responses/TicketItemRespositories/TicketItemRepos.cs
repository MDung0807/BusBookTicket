using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.Ticket.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Ticket.Responses.TicketItemRespositories;

public class TicketItemRepos : ITicketItemRepos
{

    #region -- Properties --

    private readonly AppDBContext _context;
    
    #endregion -- Properties --

    public TicketItemRepos(AppDBContext context)
    {
        this._context = context;
    }
    public Task<TicketItem> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> update(TicketItem entity)
    {
        try
        {
            _context.TicketItems.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public async Task<int> delete(TicketItem entity)
    {
        try
        { 
            _context.TicketItems.Update(entity).Property(x => x.status).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public Task<List<TicketItem>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(TicketItem entity)
    {
        try
        {
            _context.TicketItems.Entry(entity).State= EntityState.Added;
            return await _context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public Task<List<TicketItem>> getAllItem(int ticketID)
    {
        try
        {
            return _context.TicketItems.Where(x => x.ticket.ticketID == ticketID)
                .Include(x => x.ticket.bus.seats)
                .ToListAsync()?? throw new NotFoundException(TicketConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }
}