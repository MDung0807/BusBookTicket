using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Buses.Repositories.BusTypeRepositories;

public class BusTypeRepos : IBusTypeRepos
{
    #region -- Properties --
    private readonly AppDBContext _context;
    #endregion -- Properties --

    public BusTypeRepos(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<BusType> getByID(int id) 
    {
        try
        {
            return await _context.BusesType.Where(x => x.busTypeID == id)
                .Include(x => x.buses)
                .FirstAsync() ?? throw new NotFoundException(BusTypeConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> update(BusType entity)
    {
        try
        {
            _context.BusesType.Update(entity);
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> delete(BusType entity)
    {
        try
        {
            _context.BusesType.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {            
            throw new Exception(BusTypeConstants.DELETE_ERROR);
        }
    }

    public async Task<List<BusType>> getAll()
    {
        try
        {
            return await _context.Set<BusType>().ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(BusTypeConstants.ERROR);
        }
    }

    public async Task<int> create(BusType entity)
    {
        try
        {
            _context.BusesType.Add(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(BusTypeConstants.ERROR);
        }

    }
}