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
        private bool _status = false;
        #endregion -- Properties --
        #region -- Contructor --
        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }
        #endregion -- Contructor --

        public bool create(Customer entity)
        {
            try
            {
                _context.Add<Customer>(entity);
                _status = true;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
                _status = false;
            }

            return _status;
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }
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
