using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.CustomerManage.Paging;

namespace BusBookTicket.CustomerManage.Specification;

public sealed class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(CustomerPaging paging = null)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }

    public CustomerSpecification(int id, bool checkStatus = true) : base(x => x.Id == id, checkStatus: checkStatus)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
    }public CustomerSpecification(string email, bool checkStatus = true) : base(x => x.Email == email, checkStatus)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
    }
    
    
}