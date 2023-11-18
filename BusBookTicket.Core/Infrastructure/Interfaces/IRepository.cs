using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// Communication with database
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IRepository<T> where T: BaseEntity
    {
        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="userId">ID user</param>
        /// <returns>ID in entity update</returns>
        Task<T> Update(T entity, int userId);

        /// <summary>
        /// Update status entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="userId">ID user</param>
        Task<T> Delete(T entity,int userId);
        
        /// <summary>
        /// Insert entity into database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="userId">ID user</param>
        /// <returns>ID for entity in database</returns>
        Task<T> Create(T entity, int userId);

        /// <summary>
        /// Change status in entity and reference entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> ChangeStatus(T entity, int userId, int status);

        /// <summary>
        /// Check data is exist in database
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<bool> CheckIsExist(ISpecification<T> specification);
    }
}
