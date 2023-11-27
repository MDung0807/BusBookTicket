using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Application.Specification.Interfaces;
using BusBookTicket.Core.Models.Entity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BusBookTicket.Ticket.Specification;

/// <summary>
/// Specification in Ticket
/// </summary>
public sealed class TicketSpecification : BaseSpecification<Core.Models.Entity.Ticket>
{
    /// <summary>
    /// Find Ticket By Id
    /// </summary>
    /// <param name="id">Primary key in Ticket</param>
    public TicketSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.Seats);
        AddInclude(x => x.Bus.Company);
        AddInclude(x => x.Bus.BusType);
        AddInclude(x => x.Bus.BusStops);
    }

    /// <summary>
    /// Constructor find tickets
    /// </summary>
    /// <param name="dateTime">Date start</param>
    /// <param name="stationStart">Station start</param>
    /// <param name="stationEnd">Station end</param>
    public TicketSpecification(DateTime dateTime, string stationStart, string stationEnd)
      
    {
        AddInclude(x => x.Bus);
        AddInclude(x => x.Bus.BusStops);
    }
}