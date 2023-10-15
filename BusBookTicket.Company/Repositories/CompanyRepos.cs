using BusBookTicket.Common.Common.Exceptions;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
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
    
    public Company getByID(int id)
    {
        try
        {
            return _context.Companies.Where(x => x.companyID == id)
                .Include(x => x.account)
                .Include(x => x.account.role).First() ??
                   throw new NotFoundException(CompanyConstants.NOT_FOUND);
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }
    }

    public int update(Company entity)
    {
        try
        {
            int id = _context.Companies.Update(entity).Entity.companyID;
            _context.SaveChanges();
            return id;
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }

    }

    public int delete(Company entity)
    {
        try
        {
             _context.Entry(entity).Property(x => x.account.status).IsModified = true;
            foreach (var bus in entity.buses.Where(x => x.company.companyID == entity.companyID))
            {
                _context.Entry(bus).Property(x => x.status).IsModified = true;
            }
            _context.SaveChanges();
            return entity.companyID;
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_DELETE);
        }

    }

    public List<Company> getAll()
    {
        try
        {
            return _context.Set<Company>().ToList();
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_GET);
        }
    }

    public int create(Company entity)
    {
        try
        {
            int id = _context.Add(entity).Entity.companyID;
            _context.SaveChanges();
            return id;
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_CREATE);
        }

    }
}