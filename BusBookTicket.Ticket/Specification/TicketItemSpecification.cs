using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;
using CloudinaryDotNet.Actions;

namespace BusBookTicket.Ticket.Specification;

public sealed class TicketItemSpecification : BaseSpecification<TicketItem>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">id is primary key in TicketItem</param>
    /// <param name="getAll"></param>
    public TicketItemSpecification(int id) : base(x => x.Id == id)
    {
        
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ticketItemId">is primary key in TicketItem</param>
    /// <param name="ticketId">is primary key in Ticket</param>
    /// <param name="getAll">is true then get all reference</param>
    /// <param name="checkStatus"></param>
    public TicketItemSpecification(int ticketItemId, int ticketId, bool getAll = false, bool checkStatus = true) 
        : base(x => x.Ticket.Id == ticketId, checkStatus)
    {
        if (getAll)
        {
            AddInclude(x => x.BillItem);
            AddInclude(x => x.BillItem.Bill);
            return;
        }
        
        AddInclude(x => x.Ticket.Bus.Seats);
        AddInclude(x => ((TicketItem)x).Ticket.Bus.Seats); 

    }

}