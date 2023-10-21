using BusBookTicket.Common.Common;
using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Repositories.AuthRepository
{
    public interface IAuthRepository : IRepository<Account, int>
    {
        Task<bool> login(Account acc);
        Task<Account> getAccByUsername(string username, string roleName);
    }
}
