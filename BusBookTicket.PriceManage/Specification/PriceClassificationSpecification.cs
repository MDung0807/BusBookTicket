using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.PriceManage.Paging;

namespace BusBookTicket.PriceManage.Specification;

public sealed class PriceClassificationSpecification : BaseSpecification<PriceClassification>
{
    public PriceClassificationSpecification(int id = default, int companyId = default, bool checkStatus = true, bool getIsChangeStatus = false,
        PriceClassificationPaging paging = null)
    :base(x => (id == default || x.Id == id)
    && (companyId == default || x.Company.Id == companyId), checkStatus)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.Company);
            return;
        }

        if (checkStatus)
        {
            AddCriteria(x => x.Status == (int)EnumsApp.Active);
        }
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
    }

    public void FindByParam(string param, PriceClassificationPaging paging = default, bool checkStatus = true)
    {
        Criteria = x =>
            x.Company.Name.Contains( param) || x.Name.Contains(param)  ||
            x.Tickets.Any(t => t.Bus.BusNumber.Contains(param) || t.Id.ToString() == param) ||
            x.Id.ToString() == param;
        CheckStatus = checkStatus;
        if (paging != default)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
    }
}