using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.BillManage.Specification;

public class BillItemSpecification : BaseSpecification<BillItem>
{
    public BillItemSpecification(int id) : base(x => x.Bill.Id == id, false)
    {
        AddInclude(x => x.TicketItem);
        AddInclude(x => x.TicketItem.Ticket);
        AddInclude(x => x.TicketItem.Ticket.Bus);
        AddInclude(x => x.TicketItem.Ticket.Bus.Company);

    }
}