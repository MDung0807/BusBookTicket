using BusBookTicket.Core.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Repositories.RoleRepository
{
    public interface IRoleRepository
    {
        Task<Role> getRole(string roleName);
    }
}
