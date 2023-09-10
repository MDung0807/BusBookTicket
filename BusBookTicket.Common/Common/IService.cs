using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    internal interface IService<T>
    {
        T getByID(int id);
        List<T> GetAll();
        T update(T entity);
        void delete(int id);
    }
}
