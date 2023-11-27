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
    Task<T> Get(ISpecification<T> specification);
    Task<List<T>> ToList(ISpecification<T> specification);
    bool Contains(ISpecification<T> specification = null);
    bool Contains(Expression<Func<T, bool>> predicate);
    int Count(ISpecification<T> specification = null);
    int Count(Expression<Func<T, bool>> predicate);
    Task<List<T>> ToListWithSqlQuery(string sqlQuery);
}