using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Buses.Repositories.SeatRepositories;

public class SeatRepository : ISeatRepository
{
    #region -- Properties --

    private readonly AppDBContext _context;
    #endregion -- Properties --

    public SeatRepository(AppDBContext context)
    {
        this._context = context;
    }

    #region -- Public Method --

    public async Task<List<Seat>> getSeatInBus(int busID)
    {
        try
        {
            return await _context.Seats
                .Where(x => x.bus.busID == busID)
                .ToListAsync();
        }
        catch
        {
            throw new Exception(SeatConstants.ERROR);
        }
    }

    public async Task<Seat> getByID(int id)
    {
        try
        {
            return await _context.Seats
                .Where(x => x.seatID == id)
                .FirstAsync();
        }
        catch
        {
            throw new Exception(SeatConstants.ERROR);
        }
    }

    public async Task<int> update(Seat entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(SeatConstants.ERROR);
        }
    }

    public async Task<int> delete(Seat entity)
    {
        try
        {
            _context.Update<Seat>(entity).Property(x => x.status).IsModified = true;
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(SeatConstants.ERROR);
        }
    }

    public Task<List<Seat>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(Seat entity)
    {
        try
        {
             _context.Entry(entity).State  = EntityState.Added;
            return await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new Exception(SeatTypeConstants.ERROR);
        }
    }

    #endregion -- Public Method --
    
}