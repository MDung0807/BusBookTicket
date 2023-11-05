using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.CompanyManage.Specification;

public sealed class CompanySpecification : BaseSpecification<Company>
{
    public CompanySpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
    }

    public CompanySpecification()
    {
        
    }
}