using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.CompanyManage.Specification;

public sealed class CompanySpecification : BaseSpecification<Company>
{
    public CompanySpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Ward);
    }

    public CompanySpecification(int id, bool checkStatus) : base(x => x.Id == id, checkStatus)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Ward);
    }
    public CompanySpecification()
    {
        
    }
}