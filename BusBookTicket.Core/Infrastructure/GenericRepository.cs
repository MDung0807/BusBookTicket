using System.Linq.Expressions;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Common;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Core.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
{
    #region -- Properties --

    private readonly AppDBContext _context;
    private readonly DbSet<T> _dbSet;
    #endregion -- Properties --

    public GenericRepository(AppDBContext context)
    {
        this._context = context;
        this._dbSet = context.Set<T>();
    }

    public async Task<List<T>> ToList(ISpecification<T> specification)
    {
        try
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    public bool Contains(ISpecification<T> specification = null)
    {
        return Count(specification) > 0 ? true : false;
    }

    public bool Contains(Expression<Func<T, bool>> predicate)
    {
        return Count(predicate) > 0 ? true : false;
    }

    public int Count(ISpecification<T> specification = null)
    {
        return ApplySpecification(specification).Count();
    }

    public int Count(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().Where(predicate).Count();
    }

    public async Task<T> Get(ISpecification<T> specification)
    {
        try
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync()
                   ?? throw new NotFoundException(AppConstants.NOT_FOUND);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    public async Task<T> Update(T entity, int userId)
    {
        try
        {
            entity.DateUpdate = DateTime.Now;
            entity.UpdateBy = userId;
            _dbSet.Entry(entity).Property(x => x.DateCreate).IsModified = false;
            _dbSet.Entry(entity).Property(x => x.CreateBy).IsModified = false;
            
            _dbSet.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    public async Task<T> Delete(T entity, int userId)
    {
        try
        {
            entity.DateUpdate = DateTime.Now;
            entity.UpdateBy = userId;
            _dbSet.Entry(entity).Property(x => x.DateCreate).IsModified = false;
            _dbSet.Entry(entity).Property(x => x.CreateBy).IsModified = false;
            
            _dbSet.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    public async Task<T> Create(T entity, int userId)
    {
        try
        {
            entity.DateCreate = DateTime.Now;
            entity.DateUpdate = DateTime.Now;
            entity.CreateBy = userId;
            entity.UpdateBy = userId;
            _dbSet.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch
        {
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    #region -- Private Method --
    private IQueryable<T> ApplySpecification(ISpecification<T> specifications)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specifications);
    }
    #endregion -- Private Method --
}