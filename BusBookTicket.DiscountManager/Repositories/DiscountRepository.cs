using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.DiscountManager.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.DiscountManager.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly AppDBContext _context;

    public DiscountRepository(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<Discount> getByID(int id)
    {
        try
        {
            return await _context.Discounts.FirstAsync(x => x.discountID == id);
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public async Task<int> update(Discount entity)
    {
        try
        {
            _context.Update(entity); 
            return await _context.SaveChangesAsync();
        }
        catch 
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public async Task<int> delete(Discount entity)
    {
        try
        {
            _context.Entry(entity).Property(x => x.status).IsModified = true;
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public async Task<List<Discount>> getAll()
    {
        try
        {
            return await _context.Set<Discount>().ToListAsync();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }

    public async Task<int> create(Discount entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(DiscountConstants.ERROR);
        }
    }
}