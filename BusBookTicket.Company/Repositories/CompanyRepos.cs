using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.CompanyManage.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.CompanyManage.Repositories;

public class CompanyRepos : ICompanyRepos
{
    #region -- Properties --
    private readonly AppDBContext _context;
    #endregion

    public CompanyRepos(AppDBContext context)
    {
        this._context = context;
    }
    
    public async Task<Company> getByID(int id)
    {
        try
        {
            return await _context.Companies.Where(x => x.companyID == id)
                .Include(x => x.account)
                .Include(x => x.account.role).FirstAsync() ??
                   throw new NotFoundException(CompanyConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }
    }

    public async Task<int> update(Company entity)
    {
        try
        {
            _context.Companies.Update(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }

    }

    public async Task<int> delete(Company entity)
    {
        try
        {
             _context.Entry(entity).Property(x => x.account.status).IsModified = true;
            foreach (var bus in entity.buses.Where(x => x.company.companyID == entity.companyID))
            {
                _context.Entry(bus).Property(x => x.status).IsModified = true;
            }
            return await  _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_DELETE);
        }

    }

    public async Task<List<Company>> getAll()
    {
        try
        {
            return await _context.Companies.ToListAsync();
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }
    }

    public async Task<int> create(Company entity)
    {
        try
        {
            await _context.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_CREATE);
        }

    }
}