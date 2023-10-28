using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.Core.Utils;
using BusBookTicket.Ticket.DTOs.Response;
using BusBookTicket.Ticket.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Ticket.Responses.TicketRepositories;

public class TicketRepository : ITicketRepository
{
    #region -- Properties --

    private readonly AppDBContext _context;
    #endregion -- Properties --

    public TicketRepository(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<Core.Models.Entity.Ticket> getByID(int id)
    {
        try
        {
            return await _context.Tickets.Where(x => x.ticketID == id)
                .Include(x => x.bus)
                .Include(x => x.bus.company)
                .FirstAsync()?? throw new NotFoundException(TicketConstants.NOT_FOUND);
        }
        catch 
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public async Task<int> update(Core.Models.Entity.Ticket entity)
    {
        try
        {
            _context.Tickets.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public async Task<int> delete(Core.Models.Entity.Ticket entity)
    {
        try
        { 
            _context.Tickets.Update(entity).Property(x => x.status).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public Task<List<Core.Models.Entity.Ticket>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(Core.Models.Entity.Ticket entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch (System.Exception ex)
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }

    public async Task<List<Core.Models.Entity.Ticket>> getAllTicket(DateTime dateTime, string stationStart, string stationEnd)
    {
        try
        {
            List<Core.Models.Entity.Ticket> ticketResponses = await _context.Tickets
                .Where(x => x.status != (int)EnumsApp.Delete && x.status != (int)EnumsApp.Disable)
                .Where(x => x.date >= dateTime)
                .Include(x => x.bus.busStops
                    .Where(x => x.BusStation.name == stationStart && x.BusStation.name == stationEnd))
                .ToListAsync() ?? throw new NotFoundException(TicketConstants.NOT_FOUND);
            return ticketResponses;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }
}