using BusBookTicket.BillManage.Utilities;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.BillManage.Repositories.Bills;

public class BillRepository : IBillRepository
{
    private readonly AppDBContext _context;

    public BillRepository(AppDBContext context)
    {
        this._context = context;
    }
    public async Task<Bill> getByID(int id)
    {
        try
        {
            return await _context.Bills.Where(x => x.billID == id)
                .Include(x => x.billItems)
                .Include(x => x.customer)
                .Include(x => x.discount)
                .Include(x => x.busStationEnd)
                .Include(x => x.busStationStart).FirstAsync();
        }
        catch
        {
            throw new Exception(BillConstants.ERROR_GET);
        }
    }

    public Task<int> update(Bill entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> delete(Bill entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Bill>> getAll()
    {
        throw new NotImplementedException();
    }

    public async Task<int> create(Bill entity)
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