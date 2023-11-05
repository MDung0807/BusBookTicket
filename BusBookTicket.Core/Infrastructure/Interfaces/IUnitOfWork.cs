using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Core.Infrastructure.Interfaces;

/// <summary>
/// Is Communication with Database
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Save change all activity
    /// </summary>
    /// <returns></returns>
    Task SaveChangesAsync();
    
    /// <summary>
    /// Begin transaction in activity
    /// </summary>
    /// <returns></returns>
    Task BeginTransaction();
    
    /// <summary>
    /// Rollback activity when has error
    /// </summary>
    /// <returns></returns>
    Task RollbackTransactionAsync();
    
    /// <summary>
    /// GenericRepository
    /// </summary>
    /// <typeparam name="T">Is Entity, T: BaseEntity</typeparam>
    /// <returns></returns>
    IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
    
    /// <summary>
    /// Complete activity
    /// </summary>
    /// <returns></returns>
    Task<int> Complete();
}