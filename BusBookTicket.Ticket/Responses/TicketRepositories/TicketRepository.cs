using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
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
            Core.Models.Entity.Ticket ticket  =  await _context.Tickets.Where(x => x.ticketID == id)
                .Include(x => x.bus)
                .ThenInclude(x => x.seats)
                .Include(x => x.bus)
                .ThenInclude(x => x.company)
                .Include(x => x.bus)
                .ThenInclude(x => x.busStops)
                .Include(x => x.bus)
                .ThenInclude(x => x.busType)
                .FirstAsync()?? throw new NotFoundException(TicketConstants.NOT_FOUND);

            Bus bus = await _context.Buses.Where(x => x.busID == ticket.bus.busID)
                .Include(x => x.company)
                .Include(x => x.seats)
                .Include(x => x.busStops)
                .Include(x => x.busType)
                .FirstAsync();

            ticket.bus = bus;
            return ticket;
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
            _context.Tickets.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.ticketID;
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
                .Where(x => x.date.Date >= dateTime.Date)
                .Include(x => x.bus)
                .ThenInclude(x => x.busStops)
                .Where(x => x.bus.busStops.Any(p => p.BusStation.name.Contains(stationEnd)))
                .ToListAsync() ?? throw new NotFoundException(TicketConstants.NOT_FOUND);
            return ticketResponses;
        }
        catch
        {
            throw new Exception(TicketConstants.ERROR);
        }
    }
}