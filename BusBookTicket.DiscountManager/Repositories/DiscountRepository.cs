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

    public bool update(Discount entity)
    {
        try
        {
            _context.Update(entity); 
            _context.SaveChanges();
            return true;
        }
        catch 
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public bool delete(Discount entity)
    {
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            _context.SaveChanges();
            return true;
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
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

    public bool create(Discount entity)
    {
        bool status = false;
        try
        {
            _context.Add(entity);
            _context.SaveChanges();
            status = true;
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }

        return status;
    }
}