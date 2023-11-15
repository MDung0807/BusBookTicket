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
            List<T> listData = await ApplySpecification(specification).ToListAsync();
            // listData.ForEach(CheckStatus);
            return listData;
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
            var ob = await ApplySpecification(specification).FirstOrDefaultAsync()
                     ?? throw new NotFoundException(AppConstants.NOT_FOUND);
            if (ob.Status == 0)
            {
                throw new LockedResource();
            }

            return ob;
        }
        catch (LockedResource ex)
        {
            throw new LockedResource(AppConstants.LOCKED_RESOURCE);
        }
        catch (NotFoundException ex)
        {
            Console.WriteLine(ex.ToString());
            throw new NotFoundException(AppConstants.NOT_FOUND);
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    public async Task<bool> ChangeStatus(T entity, int userId, int status)
    {
        try
        {
            foreach (var ob in entity.GetType().GetProperties())
            {
                if (ob.PropertyType.IsClass && ob.GetValue(entity) != null) // Check property is a Class
                {
                    if (ob.PropertyType.GetProperty(entity.GetType().Name + "s") != null)
                    {
                        continue;
                    }
                    var statusProperty = ob.PropertyType.GetProperty("Status"); // Get property in this class
                    var updateByProperty = ob.PropertyType.GetProperty("UpdateBy"); // Get property in this class
                    var modifyDateProperty = ob.PropertyType.GetProperty("DateUpdate"); // Get property in this class

                    // Check status property is exists
                    if (statusProperty != null)
                    {
                        //check status property is type of int
                        if (statusProperty.PropertyType == typeof(int))
                        {
                            //Update status
                            statusProperty.SetValue(ob.GetValue(entity), status);
                            updateByProperty?.SetValue(ob.GetValue(entity), userId);
                            modifyDateProperty?.SetValue(ob.GetValue(entity), userId);
                        }
                    }
                }
            }
            entity.Status = 0;
            _context.Entry(entity).Property(x => x.Status).IsModified = true;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    #region -- Private Method --
    private IQueryable<T> ApplySpecification(ISpecification<T> specifications)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specifications);
    }

    private void CheckStatus(T data)
    {
        if (data.Status == (int) EnumsApp.Delete)
        {
            throw new StatusException(AppConstants.NOT_EXIST);
        }
                
        else if (data.Status == (int)EnumsApp.Waiting)
        {
            throw new StatusException(AppConstants.WAITING);
        }

        else if (data.Status == (int)EnumsApp.Disable)
        {
            throw new StatusException(AppConstants.NOT_USED);
        }
    }
    #endregion -- Private Method --
}