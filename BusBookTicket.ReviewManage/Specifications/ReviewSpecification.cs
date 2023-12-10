using BusBookTicket.Core.Application.Specification;
using BusBookTicket.ReviewManage.Paging;

namespace BusBookTicket.ReviewManage.Specifications;

public class ReviewSpecification : BaseSpecification<Core.Models.Entity.Review>
{
    public ReviewSpecification(int busId, ReviewPaging paging = null, bool checkStatus = true)
        : base(x => x.Bus.Id == busId, checkStatus: checkStatus)
    {
        
    }
}