using BusBookTicket.Common.Common.Exceptions;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.Ranks.DTOs.Responses;
using BusBookTicket.Ranks.Utilities;

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
    public Rank getByID(int id)
    {
        try
        {
            return _context.Ranks.First(x => x.rankID == id) ?? throw new NotFoundException(RankConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(RankConstants.ERROR);
        }
    }

    public int update(Rank entity)
    {
        int id;
        try
        {
            id = _context.Update(entity).Entity.rankID;
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception(RankConstants.ERROR);
        }

        return id;
    }

    public int delete(Rank entity)
    {
        int id;
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception(RankConstants.ERROR);
        }

        return entity.rankID;
    }

    public List<Rank> getAll()
    {
        List<Rank> ranks = new List<Rank>();
        ranks = _context.Set<Rank>().ToList();
        return ranks;
    }

    public int create(Rank entity)
    {
        int id;
        try
        {
            id = _context.Add(entity).Entity.rankID;
            _context.AddAsync(entity);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(RankConstants.ERROR);
        }

        return id;
    }
}