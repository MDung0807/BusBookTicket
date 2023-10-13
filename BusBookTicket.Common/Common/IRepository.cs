using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusBookTicket.Common.Models.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BusBookTicket.Common.Common
{
    public interface IRepository<T>
    {
        T getByID(int id);
        bool update(T entity);
        bool delete(T entity);
        List<T> getAll();
        bool create (T entity);
    }
}
