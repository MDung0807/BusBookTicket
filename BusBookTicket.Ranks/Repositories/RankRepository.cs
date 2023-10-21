using BusBookTicket.Common.Common.Exceptions;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Ranks.Repositories;

public class RankRepository : IRankRepository
{
    #region -- Properties
    private AppDBContext _context;
    #endregion

    public RankRepository(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<Rank> getByID(int id)
    {
        try
        {
            return await _context.Ranks.FirstAsync(x => x.rankID == id) ?? throw new NotFoundException(RankConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(RankConstants.ERROR);
        }
    }

    public async Task<int> update(Rank entity)
    {
        try
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(RankConstants.ERROR);
        }
    }

    public async Task<int> delete(Rank entity)
    {
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        catch 
        {
            throw new Exception(RankConstants.ERROR);
        }
    }

    public async Task<List<Rank>> getAll()
    {
        List<Rank> ranks = new List<Rank>();
        ranks = await _context.Set<Rank>().ToListAsync();
        return ranks;
    }

    public async Task<int> create(Rank entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(RankConstants.ERROR);
        }
    }
}