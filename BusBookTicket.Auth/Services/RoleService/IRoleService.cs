using BusBookTicket.Common.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBookTicket.Auth.Services.RoleService
{
    public interface IRoleService
    {
        Role getRole(string roleName);
    }
}
