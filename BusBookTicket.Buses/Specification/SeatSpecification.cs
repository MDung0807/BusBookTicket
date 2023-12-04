using BusBookTicket.Core.Application.Specification;
using BusBookTicket.Core.Models.Entity;

namespace BusBookTicket.Buses.Specification;

public class SeatSpecification : BaseSpecification<Seat>
{
    public SeatSpecification(int id) : base(x => x.Id == id){}
    
    public SeatSpecification(int id, int busId) : 
        base(x => x.Bus.Id == busId){}
}