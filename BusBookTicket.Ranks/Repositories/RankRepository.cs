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

    public bool update(Rank entity)
    {
        bool status = false;
        try
        {
            _context.Update(entity);
            _context.SaveChanges();
            status = true;
        }
        catch
        {
            throw new Exception(RankConstants.ERROR);
        }

        return status;
    }

    public bool delete(Rank entity)
    {
        bool status = false;
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            _context.SaveChanges();
            status = true;
        }
        catch 
        {
            throw new Exception(RankConstants.ERROR);
        }

        return status;
    }

    public List<Rank> getAll()
    {
        List<Rank> ranks = new List<Rank>();
        ranks = _context.Set<Rank>().ToList();
        return ranks;
    }

    public bool create(Rank entity)
    {
        bool status = false;
        try
        {
            _context.Add(entity);
            _context.AddAsync(entity);
            _context.SaveChanges();
            status = true;
        }
        catch (Exception e)
        {
            throw new Exception(RankConstants.ERROR);
        }

        return status;
    }
}