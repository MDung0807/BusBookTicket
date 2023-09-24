using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Repositories
{
    public class AccountResponse : IAccountService
    {
        #region -- Properties --
        private readonly AppDBContext _context;
        #endregion -- Properties --
        public AccountResponse(AppDBContext context) 
        {
            _context = context;
        }
        public bool create(Account entity)
        {
            bool status = false;
             _context.Add(entity);
             status = true;
            return status;
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> getAll()
        {
            throw new NotImplementedException();
        }

        public Account getByID(int id)
        {
            throw new NotImplementedException();
        }

        public Account update(Account entity)
        {
            throw new NotImplementedException();
        }
    }
}
