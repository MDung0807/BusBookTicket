using BusBookTicket.CompanyManage.Paging;
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

    public CompanySpecification(int id, bool checkStatus = true, bool getAll = true) : base(x => x.Id == id,
        checkStatus)
    {
        if (!getAll)
        {
            AddInclude(x => x.Account);
            return;
        }

        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Ward);
    }

    public CompanySpecification(bool checkStatus = true, CompanyPaging paging = null) : base(null, checkStatus)
    {

    }

    public CompanySpecification()
    {

    }

    public void FindByParam(string param, CompanyPaging paging = default, bool checkStatus= true)
    {
        Criteria = x => x.Name.Contains(param) || x.Ward.FullName.Contains(param) ||
                        x.Ward.District.FullName.Contains(param) || x.Ward.District.Province.FullName.Contains(param);
    }

}