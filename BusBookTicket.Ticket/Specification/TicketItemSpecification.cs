using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Ticket.Specification;

public sealed class TicketItemSpecification : BaseSpecification<TicketItem>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">id is primary key in TicketItem</param>
    public TicketItemSpecification(int id) : base(x => x.Id == id){}

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ticketItemId">is primary key in TicketItem</param>
    /// <param name="ticketId">is primary key in Ticket</param>
    public TicketItemSpecification(int ticketItemId, int ticketId) : base(x => x.Ticket.Id == ticketId)
    {
        AddInclude(x => x.Ticket.Bus.Seats);    
    }

}