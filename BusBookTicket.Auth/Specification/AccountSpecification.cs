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

    public AccountSpecification(string username, bool checkStatus = true): base(x => x.Username == username, checkStatus)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.Customer);
        AddInclude(x => x.Role);
    }

    public AccountSpecification(int id, bool checkStatus = true, bool isDelete = false)
        : base(x => x.Id == id 
                    && ((isDelete && x.Status == (int)EnumsApp.Waiting) || true),
            checkStatus: checkStatus)
    {
        
    }
}