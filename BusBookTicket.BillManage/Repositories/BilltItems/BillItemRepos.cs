using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.BillManage.Repositories.BillItems;

public class BillItemRepos : IBillItemRepos
{
    private readonly AppDBContext _context;

    public BillItemRepos(AppDBContext context)
    {
        this._context = context;
    }

    public async Task<List<BillItem>> getAllItems(int billID)
    {
        try
        {
            return await _context.BillItems.Where(x => 
                x.bill != null && x.bill.billID == billID)
                .ToListAsync();
        }
        catch 
        {
            throw new Exception(BillConstants.ERROR_GET);
        }
    }
    public Task<BillItem> getByID(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> update(BillItem entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> delete(BillItem entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<BillItem>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(BillItem entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_CREATE);
        }
    }
}