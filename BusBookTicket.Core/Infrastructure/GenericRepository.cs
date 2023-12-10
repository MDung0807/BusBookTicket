using System.Collections;
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
using Microsoft.EntityFrameworkCore.Storage;

namespace BusBookTicket.Core.Infrastructure;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
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

    public async Task<bool> Contains(ISpecification<T> specification = null)
    {
        return await Count(specification) > 0 ? true : false;
    }

    public async Task<bool> Contains(Expression<Func<T, bool>> predicate)
    {
        return await Count(predicate) > 0 ? true : false;
    }

    public async Task<int> Count(ISpecification<T> specification = null)
    {
        return await ApplySpecification(specification).CountAsync();
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).CountAsync();
    }

    public async Task<T> Get(ISpecification<T> specification)
    {
        try
        {
            var ob = await ApplySpecification(specification).FirstOrDefaultAsync();
                     // ?? throw new NotFoundException(AppConstants.NOT_FOUND);
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
            _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.Entry(entity).Property(x => x.DateCreate).IsModified = false;
            _dbSet.Entry(entity).Property(x => x.CreateBy).IsModified = false;
            
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
            _dbSet.Entry(entity).State = EntityState.Modified;
            _dbSet.Entry(entity).Property(x => x.DateCreate).IsModified = false;
            _dbSet.Entry(entity).Property(x => x.CreateBy).IsModified = false;
            
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

    public async Task<bool> ChangeStatus(object entity, int userId, int status)
    {
        try
        {
            List<string> checkedObject = new List<string>();
            await ChangeStatusImpl(entity, userId, status, checkedObject);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw;
        }
    }

    public async Task<bool> CheckIsExist(ISpecification<T> specification)
    {
        var ob = await ApplySpecification(specification).FirstOrDefaultAsync();
        if (ob == null)
            return false;
        return true;
    }

    public async Task<T> CreateOrUpdate(T entity, int userId)
    {
        try
        {
            entity.DateCreate = DateTime.Now;
            entity.DateUpdate = DateTime.Now;
            entity.CreateBy = userId;
            entity.UpdateBy = userId;
            _dbSet.Entry(entity).State = entity.Id > 0 ? EntityState.Modified : EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw new ExceptionDetail(AppConstants.ERROR);
        }
    }

    #region -- Private Method --
    private IQueryable<T> ApplySpecification(ISpecification<T> specifications)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specifications, _dbSet);
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

    private async Task<bool> ChangeStatusImpl(object entity, int userId, int status, List<string> checkedObject)
    {
        if (!checkedObject.Contains(entity.GetType().Name))
            checkedObject.Add(entity.GetType().Name);
        foreach (var ob in entity.GetType().GetProperties())
        {
            if (ob.PropertyType.IsClass && ob.GetValue(entity) != null  &&  ob.PropertyType != typeof(string)) // Check property is a Class
            {
                if (ob.PropertyType.IsGenericType)
                {
                    // Kiểm tra xem ob có phải là List<> hoặc HashSet<> không
                    var genericType = ob.PropertyType.GetGenericTypeDefinition();
                    if (genericType == typeof(List<>) || genericType == typeof(HashSet<>))
                    {
                        var collection = (IEnumerable)ob.GetValue(entity);
                
                        foreach (var item in collection)
                        {
                            if (item != null && item.GetType().IsClass)
                            {
                                await ChangeStatusImpl(item, userId, status, checkedObject);
                            }
                        }
                    }
                }
                
                else
                {
                    if (!checkedObject.Contains(ob.Name))
                    {
                        checkedObject.Add(ob.Name);
                        await ChangeStatusImpl(ob.GetValue(entity), userId, status, checkedObject);
                    }
                }
            }
            else
            {
                if (ob.Name == "Status")
                {
                    ob.SetValue(entity, status);
                }

                else if (ob.Name == "UpdateBy")
                {
                    ob.SetValue(entity, userId);
                }
                else if (ob.Name == "DateUpdate")
                {
                    ob.SetValue(entity, DateTime.Now);
                }
            }
        }

        // entity. = status;
        // entity.UpdateBy = userId;
        // entity.DateUpdate = DateTime.Now;
        // _context.Entry(entity).Property(x => x.Status).IsModified = true;
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion -- Private Method --
}