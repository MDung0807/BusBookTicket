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
    }
}
