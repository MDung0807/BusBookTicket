using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Core.Common
{
    /// <summary>
    /// Service for API, this is interface common
    /// </summary>
    /// <typeparam name="Create">form create</typeparam>
    /// <typeparam name="Update">form update</typeparam>
    /// <typeparam name="ID">ID in entity</typeparam>
    /// <typeparam name="Response">response to view</typeparam>
    public interface IService<Create, Update, ID, Response>
    {
        /// <summary>
        /// Get data by id
        /// </summary>
        /// <param name="id">Is ID in entity</param>
        /// <returns>Entity has id = params</returns>
        Task<Response> getByID(ID id);
        Task<List<Response>> getAll();
        Task<bool> update(Update entity, ID id);
        Task<bool> delete(ID id);
        Task<bool> create(Create entity);
    }
}
