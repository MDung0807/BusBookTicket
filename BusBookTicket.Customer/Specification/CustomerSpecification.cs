using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.CustomerManage.Specification;

public sealed class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification()
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
        int pageSize = 2;
        int page = (1 -1)* pageSize;
        ApplyPaging(page, pageSize);
    }

    public CustomerSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
    }
}