using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    public interface IService<Create, Update, Response>
    {
        Response getByID(int id);
        List<Response> GetAll();
        Response update(Update entity);
        bool delete(int id);
        bool create(Create entity);
    }
}
