namespace BusBookTicket.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// Service for API, this is interface common
    /// </summary>
    /// <typeparam name="TCreate">form create</typeparam>
    /// <typeparam name="TUpdate">form update</typeparam>
    /// <typeparam name="TId">ID in entity</typeparam>
    /// <typeparam name="TResponse">response to view</typeparam>
    public interface IService<in TCreate, in TUpdate, in TId, TResponse>
    {
        /// <summary>
        /// Get data by id
        /// </summary>
        /// <param name="id">Is ID in entity</param>
        /// <returns>Entity has id = params</returns>
        Task<TResponse> GetById(TId id);
        /// <summary>
        /// Get all data Entity in database
        /// </summary>
        /// <returns></returns>
        Task<List<TResponse>> GetAll();
        
        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="id">Primary key in Entity</param>
        /// <param name="userId">ID User</param>
        /// <returns></returns>
        Task<bool> Update(TUpdate entity, TId id, int userId);
        
        /// <summary>
        /// Update status to entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<bool> Delete(TId id, int userId);

        /// <summary>
        /// Handling insert data into database
        /// </summary>
        /// <param name="entity">Is Entity</param>
        /// <param name="userId">Is Id for User</param>
        /// <returns></returns>
        Task<bool> Create(TCreate entity, int userId);

        Task<bool> ChangeIsActive(TId id, int userId);
        Task<bool> ChangeIsLock(TId id, int userId);
        Task<bool> ChangeIsWaiting(TId id, int userId);
        Task<bool> ChangeIsDisable(TId id, int userId);

        Task<bool> CheckIsExistById(TId id);
        Task<bool> CheckIsExistByParam(string param);
    }
}
