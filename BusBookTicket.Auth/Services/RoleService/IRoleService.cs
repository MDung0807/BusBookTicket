using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Auth.Services.RoleService
{
    public interface IRoleService
    {
         Task<Role> getRole(string roleName);
    }
}
