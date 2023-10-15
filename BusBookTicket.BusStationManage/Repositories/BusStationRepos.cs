using BusBookTicket.BusStationManage.Utils;
using BusBookTicket.Common.Common.Exceptions;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
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
    public BusStation getByID(int id)
    {
        try
        {
            return _context.BusStations.Where(x => x.busStationID == id)
                .Include(x => x.busStops)
                .First() ?? throw new NotFoundException(BusStationConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(BusStationConstants.GET_ERROR);
        }
    }

    public int update(BusStation entity)
    {
        try
        {
            int id = _context.Update(entity).Entity.busStationID;
            return id;
        }
        catch
        {            
            throw new Exception(BusStationConstants.UPDATE_ERROR);
        }
    }

    public int delete(BusStation busStation)
    {
        try
        {
            int id = _context.BusStations.Update(busStation).Entity.busStationID;
            _context.SaveChanges();
            return id;
        }
        catch
        {            
            throw new Exception(BusStationConstants.DELETE_ERROR);
        }

    }

    public List<BusStation> getAll()
    {
        try
        {
            return _context.Set<BusStation>().ToList();
        }
        catch (Exception e)
        {
            throw new Exception(BusStationConstants.ERROR);
        }
    }

    public int create(BusStation entity)
    {
        try
        {
            int id = _context.BusStations.Add(entity).Entity.busStationID;
            _context.SaveChanges();
            return id;
        }
        catch
        {
            throw new Exception(BusStationConstants.CREATE_ERROR);
        }

    }
}