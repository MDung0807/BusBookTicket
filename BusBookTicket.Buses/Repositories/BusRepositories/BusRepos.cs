using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Buses.Repositories.BusTypeRepositories;

public class BusRepos : IBusRepos
{
    #region -- Properties --
    private readonly AppDBContext _context;
    #endregion -- Properties --

    public BusRepos(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<Bus> getByID(int id) 
    {
        try
        {
            return await _context.Buses.Where(x => x.busID == id)
                .Include(x => x.company)
                .Include(x => x.busStops)
                .FirstAsync() ?? throw new NotFoundException(BusTypeConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> update(Bus entity)
    {
        try
        {
            _context.Buses.Entry(entity).State = EntityState.Modified;
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> delete(Bus entity)
    {
        try
        {
            _context.Buses.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {            
            throw new Exception(BusTypeConstants.DELETE_ERROR);
        }
    }

    public async Task<List<Bus>> getAll()
    {
        try
        {
            return await _context.Set<Bus>().ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> create(Bus entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.busID;    
        }
        catch
        {
            throw new Exception(BusTypeConstants.ERROR);
        }

    }

    public async Task<int> createStopStation(BusStop busStop)
    {
        try
        {
            _context.BusStops.Entry(busStop).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return busStop.busStopID;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("ERROR");
        }
    }
}