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

    public bool update(BusStation entity)
    {
        bool status;
        try
        {
            _context.Update(entity);
            status = true;
        }
        catch
        {            
            status = false;
            throw new Exception(BusStationConstants.UPDATE_ERROR);
        }

        return status;
    }

    public bool delete(BusStation busStation)
    {
        bool status = false;
        try
        {
            _context.BusStations.Update(busStation);
            _context.SaveChanges();
            status = true;
        }
        catch
        {            
            status = false;
            throw new Exception(BusStationConstants.DELETE_ERROR);
        }

        return status;
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

    public bool create(BusStation entity)
    {
        bool status = false;
        try
        {
            _context.BusStations.Add(entity);
            _context.SaveChanges();
            status = true;
        }
        catch
        {
            status = false;
            throw new Exception(BusStationConstants.CREATE_ERROR);
        }

        return status;
    }
}