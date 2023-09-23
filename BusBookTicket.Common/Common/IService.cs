using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Common.Common
{
    public interface IService<Create, Update, Resonse>
    {
        Resonse getByID(int id);
        List<Resonse> GetAll();
        Resonse update(Update entity);
        bool delete(int id);
        bool create(Create entity);
    }
}
