using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public sealed class BusSpecification : BaseSpecification<Bus>
{
    public BusSpecification(int id, bool checkStatus = true, bool getChangeStatus = false) : base(x => x.Id == id, checkStatus: checkStatus)
    {
        if (getChangeStatus)
        {
            AddInclude(x => x.Seats);
            AddInclude(x => x.BusStops);
            return;
        }
        AddInclude(x => x.Company);
        AddInclude(x => x.BusStops);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }


public BusSpecification(int id, int idCompany, bool checkStatus = true) 
        : base(x => x.Id == id 
                    && x.Company.Id == idCompany,
            checkStatus: checkStatus)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.BusStops);
        AddInclude(x => x.BusType);
        AddInclude(x => x.Seats);
    }

    public BusSpecification(bool checkStatus = true): base(checkStatus:checkStatus)
    {
        AddInclude(x => x.Company);
        AddInclude(x => x.BusType);
    }
}