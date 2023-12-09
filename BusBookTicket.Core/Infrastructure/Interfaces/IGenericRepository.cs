using System.Linq.Expressions;
using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Core.Infrastructure.Interfaces;

/// <summary>
/// Generic Repository
/// </summary>
/// <typeparam name="T">T is Entity</typeparam>
public interface IGenericRepository<T>: IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get data from database use specification
    /// </summary>
    /// <param name="specification">specification</param>
    /// <returns></returns>
    Task<T> Get(ISpecification<T> specification);
    
    /// <summary>
    /// Get list data from database use specification
    /// </summary>
    /// <param name="specification">specification</param>
    /// <returns></returns>
    Task<List<T>> ToList(ISpecification<T> specification);
    
    /// <summary>
    /// Check Contains data in database
    /// </summary>
    /// <param name="specification">specification</param>
    /// <returns value="bool">true or false. True is contains</returns>
    Task<bool> Contains(ISpecification<T> specification = null);
    
    /// <summary>
    /// Check Contains data in database
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <example>x => x.type == "auto"</example>
    /// <returns value="bool">true or false. True is contains</returns>

    Task<bool> Contains(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Count item in database
    /// </summary>
    /// <param name="specification">specification</param>
    /// <returns value="int"></returns>
    Task<int> Count(ISpecification<T> specification = null);
    
    /// <summary>
    /// Count item in database
    /// </summary>
    /// <param name="predicate">predicate</param>
    /// <returns value="int"></returns>
    Task<int> Count(Expression<Func<T, bool>> predicate);
}