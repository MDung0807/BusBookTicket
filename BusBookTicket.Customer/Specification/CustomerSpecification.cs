using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.Paging;

namespace BusBookTicket.CustomerManage.Specification;

public sealed class CustomerSpecification : BaseSpecification<Customer>
{
    public CustomerSpecification(CustomerPaging paging = null, bool checkStatus = true) : base(x => x.Account.Role.RoleName == AppConstants.CUSTOMER, checkStatus)
    {
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }

    public CustomerSpecification(int id, bool checkStatus = true, bool getAll = true) : base(x => x.Id == id, checkStatus: checkStatus)
    {
        if (!getAll)
        {
            AddInclude(x => x.Account);
            return;
        }
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
        AddInclude(x => x.Ward);
    }public CustomerSpecification(string email, bool checkStatus = true, bool isDelete = false) 
        : base(x => x.Email == email 
                    || (checkStatus == false && isDelete == true && x.Status == (int)EnumsApp.Waiting), checkStatus)
    {
        if (isDelete)
        {
            AddInclude(x => x.Account);
            return;
        }
        AddInclude(x => x.Account);
        AddInclude(x => x.Account.Role);
        AddInclude(x => x.Rank);
    }

    public void FindByParam(string param, CustomerPaging paging = default, bool checkStatus = true)
    {
        Criteria = x =>
            x.FullName.Contains(param) || x.Ward.FullName.Contains(param) || x.Ward.District.FullName.Contains(param) ||
            x.Ward.District.Province.FullName.Contains(param);

        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }

        CheckStatus = checkStatus;
    }
}