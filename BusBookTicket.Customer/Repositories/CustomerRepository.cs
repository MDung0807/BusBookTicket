using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.CustomerManage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region -- Properties --
        private readonly AppDBContext _context;
        #endregion -- Properties --
        #region -- Contructor --
        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }

        public bool create(Customer entity)
        {
            throw new NotImplementedException();
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion -- Contructor --
        public List<Customer> getAll()
        {
            throw new NotImplementedException();
        }

        public Customer getByID(int id)
        {
            throw new NotImplementedException();
        }

        public Customer update(Customer entity)
        {
            throw new NotImplementedException();
        }

    
    }
}
