using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    public interface IRepository<T>
    {
        T getByID(int id);
        T update(T entity);
        bool delete(int id);
        List<T> getAll();
        bool create (T entity);
    }
}
