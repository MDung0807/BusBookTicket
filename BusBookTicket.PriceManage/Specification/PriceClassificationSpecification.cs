using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
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

        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
        AddInclude(x => x.Company);
    }
}