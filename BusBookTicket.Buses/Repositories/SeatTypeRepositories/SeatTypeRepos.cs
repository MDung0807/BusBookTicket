using BusBookTicket.Buses.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Buses.Repositories.SeatTypeRepositories;

public class SeatTypeRepos : ISeatTypeRepos
{
    #region -- Properties --
    private readonly AppDBContext _context;
    #endregion -- Properties --

    public SeatTypeRepos(AppDBContext context)
    {
        this._context = context;
    }

    #region -- Public Method --

    public async Task<List<SeatType>> getAll(int idCompany)
    {
        try
        {
            return await _context.SeatTypes
                .Where(x => x.Company.companyID == idCompany || x.Company.companyID == 0)
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(SeatTypeConstants.ERROR);
        }
    }

    public async Task<SeatType> getByID(int id)
    {
        try
        {
            return await _context.SeatTypes.Where(x => x.typeID == id)
                .Include(x => x.Company)
                .FirstAsync() ?? throw new NotFoundException(SeatTypeConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(SeatTypeConstants.ERROR);
        }
    }

    public async Task<int> update(SeatType entity)
    {
        try
        {
            _context.SeatTypes.Update(entity);
            int id = await _context.SaveChangesAsync();
            return id;
        }
        catch
        {            
            throw new Exception(SeatTypeConstants.ERROR);
        }
    }

    public async Task<int> delete(SeatType entity)
    {
        try
        {
            _context.SeatTypes.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {            
            throw new Exception(SeatTypeConstants.DELETE_ERROR);
        }
    }

    public Task<List<SeatType>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(SeatType entity)
    {
        try
        {
            _context.SeatTypes.Add(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(SeatTypeConstants.ERROR);
        }
    }

    #endregion -- Public Method --
}