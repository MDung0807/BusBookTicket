using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Auth.Specification;

public sealed class AccountSpecification : BaseSpecification<Account>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="username">Username in account</param>
    /// <param name="roleName">Role name</param>
    /// <param name="checkStatus"></param>
    public AccountSpecification(string username, string roleName, bool checkStatus = true) : base(x => x.Username == username, checkStatus)
    {
        if (roleName == AppConstants.COMPANY)
            AddInclude(x => x.Company);
        else
        {
            AddInclude(x => x.Customer);
        }
        AddInclude(x => x.Role);
    }
}