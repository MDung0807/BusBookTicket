using BusBookTicket.BusStationManage.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.BusStationManage.Repositories;

public class BusStationRepos : IBusStationRepos
{
    #region -- Propertíies --
    private AppDBContext _context;
    #endregion -- Propertíies --

    #region -- Constructor --

    public BusStationRepos(AppDBContext context)
    {
        this._context = context;
    }
    #endregion -- Constructor --
    public async Task<BusStation>getByID(int id)
    {
        try
        {
            return await _context.BusStations.Where(x => x.busStationID == id)
                .Include(x => x.busStops)
                .FirstAsync() ?? throw new NotFoundException(BusStationConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(BusStationConstants.GET_ERROR);
        }
    }

    public async Task<int>update(BusStation entity)
    {
        try
        {
            _context.BusStations.Update(entity);
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(BusStationConstants.UPDATE_ERROR);
        }
    }

    public async Task<int> delete(BusStation busStation)
    {
        try
        {
            _context.BusStations.Update(busStation);
            return await _context.SaveChangesAsync();
        }
        catch
        {            
            throw new Exception(BusStationConstants.DELETE_ERROR);
        }

    }

    public async Task<List<BusStation>> getAll()
    {
        try
        {
            return await _context.Set<BusStation>().ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(BusStationConstants.ERROR);
        }
    }

    public async Task<int> create(BusStation entity)
    {
        try
        {
            _context.BusStations.Add(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(BusStationConstants.CREATE_ERROR);
        }

    }
}