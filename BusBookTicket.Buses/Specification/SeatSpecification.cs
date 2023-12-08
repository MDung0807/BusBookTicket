using BusBookTicket.Buses.Paging.Seat;
using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class SeatSpecification : BaseSpecification<Seat>
{
    public SeatSpecification(int id) : base(x => x.Id == id){}

    public SeatSpecification(int? id = null, int? busId = null, SeatPaging paging = null, bool checkStatus = true) :
        base(x => x.Bus.Id == busId, checkStatus:checkStatus)
    {
        if (paging != null)
        {
            ApplyPaging(paging.PageIndex, paging.PageSize);
        }
    }
}