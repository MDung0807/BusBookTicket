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

    public bool update(Company entity)
    {
        bool status = false;
        try
        {
            _context.Companies.Update(entity);
            _context.SaveChanges();
            status = true;
        }
        catch
        {
            status = false;
            throw new Exception(CompanyConstants.ERROR_GET);
        }

        return status;
    }

    public bool delete(Company entity)
    {
        bool status = false;
        try
        {
            _context.Entry(entity).Property(x => x.account.status).IsModified = true;
            foreach (var bus in entity.buses.Where(x => x.company.companyID == entity.companyID))
            {
                _context.Entry(bus).Property(x => x.status).IsModified = true;
            }
            _context.SaveChanges();
            status = true;
        }
        catch
        {
            throw new Exception(CompanyConstants.ERROR_DELETE);
        }

        return status;
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

    public bool create(Company entity)
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
            status = false;
            throw new Exception(CompanyConstants.ERROR_CREATE);
        }

        return status;
    }
}