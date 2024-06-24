namespace BusBookTicket.Core.Infrastructure.Interfaces
{
    /// <summary>
    /// Service for API, this is interface common
    /// </summary>
    /// <typeparam name="TCreate">form create</typeparam>
    /// <typeparam name="TUpdate">form update</typeparam>
    /// <typeparam name="TId">ID in entity</typeparam>
    /// <typeparam name="TResponse">response to view</typeparam>
    /// <typeparam name="TPaging"></typeparam>
    /// <typeparam name="TPagingResult"></typeparam>
    public interface IService<in TCreate, in TUpdate, in TId, TResponse, in TPaging, TPagingResult>
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

        /// <summary>
        /// Change status is active
        /// </summary>
        /// <param name="id">primary key entity</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Task<bool> ChangeIsActive(TId id, int userId);
        
        /// <summary>
        /// Change status is Lock
        /// </summary>
        /// <param name="id">primary key entity</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Task<bool> ChangeIsLock(TId id, int userId);
        
        /// <summary>
        /// Change status is Waiting
        /// </summary>
        /// <param name="id">primary key entity</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Task<bool> ChangeToWaiting(TId id, int userId);
        
        /// <summary>
        /// Change status is diable
        /// </summary>
        /// <param name="id">primary key entity</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        Task<bool> ChangeToDisable(TId id, int userId);

        /// <summary>
        /// Check is exist
        /// </summary>
        /// <param name="id">Primary key entity</param>
        /// <returns></returns>
        Task<bool> CheckToExistById(TId id);
        
        /// <summary>
        /// Check is exist
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<bool> CheckToExistByParam(string param);

        /// <summary>
        /// Get all item with role admin.
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        Task<TPagingResult> GetAllByAdmin(TPaging pagingRequest);

        /// <summary>
        /// Get all item, but have paging
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        Task<TPagingResult> GetAll(TPaging pagingRequest);

        /// <summary>
        /// Get all item in master
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <param name="idMaster">id master</param>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        Task<TPagingResult> GetAll(TPaging pagingRequest, TId idMaster, bool checkStatus = false);

        /// <summary>
        /// Permanently deleted in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteHard(TId id);

    }
}
