using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Repositories;
using BusBookTicket.Common.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services
{
    public class AuthService : IAuthService
    {
        public bool create(AccountRequest entity)
        {
            throw new NotImplementedException();
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountResponse getByID(int id)
        {
            throw new NotImplementedException();
        }

        public AccountResponse update(AccountRequest entity)
        {
            throw new NotImplementedException();
        }
    }
}
