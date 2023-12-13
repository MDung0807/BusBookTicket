using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Specification;

public class BillItemSpecification : BaseSpecification<BillItem>
{
    public BillItemSpecification(int billId, bool checkStatus = false, bool getIsChangeStatus = false) 
        : base(x => x.Bill.Id == billId, false)
    {
        if (getIsChangeStatus)
        {
            AddInclude(x => x.Bill);
            return;
        }
        AddInclude(x => x.TicketItem);
        AddInclude(x => x.TicketItem.Ticket);
        AddInclude(x => x.TicketItem.Ticket.Bus);
        AddInclude(x => x.TicketItem.Ticket.Bus.Company);

    }
}