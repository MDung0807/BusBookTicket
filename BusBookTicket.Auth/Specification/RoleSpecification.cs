using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Auth.Specification;

public class RoleSpecification : BaseSpecification<Role>
{
    public RoleSpecification(string roleName) :base (x => x.RoleName == roleName){}
}