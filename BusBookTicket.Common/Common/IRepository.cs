using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BusBookTicket.Common.Common
{
    /// <summary>
    /// Communication with database
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <typeparam name="ID">ID in Entity</typeparam>
    public interface IRepository<T, ID> where T: class
    {
        /// <summary>
        /// Get data by id
        /// </summary>
        /// <param name="id">Is ID in entity</param>
        /// <returns>Entity has id = params</returns>
        Task<T> getByID(int id);
        
        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ID in entity update</returns>
        Task<ID> update(T entity);
        
        /// <summary>
        /// Update status entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ID in entity</returns>
        Task<ID> delete(T entity);
        
        /// <summary>
        /// Get all data entity in database
        /// </summary>
        /// <returns>List data</returns>
        Task<List<T>> getAll();
        
        /// <summary>
        /// Insert entity into database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ID for entity in database</returns>
        Task<ID> create (T entity);
    }
}
