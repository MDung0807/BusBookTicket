using BusBookTicket.Core.Common;
using BusBookTicket.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
    }
}
