using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.DiscountManager.Utilities;

namespace BusBookTicket.DiscountManager.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDBContext _context;

    public DiscountRepository(AppDBContext context)
    {
        this._context = context;
    }
    public Discount getByID(int id)
    {
        try
        {
            return _context.Discounts.First(x => x.discountID == id);
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public int update(Discount entity)
    {
        int id;
        try
        {
            id = _context.Update(entity).Entity.discountID; 
            _context.SaveChanges();
        }
        catch 
        {
            throw new Exception(DiscountConstants.ERROR);
        }

        return id;
    }

    public int delete(Discount entity)
    {
        int id;
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }

        return entity.discountID;
    }

    public List<Discount> getAll()
    {
        try
        {
            return _context.Set<Discount>().ToList();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public int create(Discount entity)
    {
        int id;
        try
        {
            id = _context.Add(entity).Entity.discountID;
            _context.SaveChanges();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }

        return id;
    }
}