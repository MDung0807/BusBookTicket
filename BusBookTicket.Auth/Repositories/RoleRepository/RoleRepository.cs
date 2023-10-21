using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Auth.Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDBContext _dbContext;

        public RoleRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Role> getRole(string roleName)
        {
            return await _dbContext.Roles.Where(x => x.roleName == roleName).FirstAsync();
        }
    }
}
