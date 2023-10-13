using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    public interface IService<Create, Update, ID, Response>
    {
        Response getByID(ID id);
        List<Response> getAll();
        bool update(Update entity, ID id);
        bool delete(ID id);
        bool create(Create entity);
    }
}
