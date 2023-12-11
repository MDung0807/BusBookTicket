using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class TicketStationDtoValidator : AbstractValidator<TicketStationDto>
{
    public TicketStationDtoValidator()
    {
        RuleFor(x => x.IndexStation)
            .GreaterThanOrEqualTo(0).WithMessage("is Greater 0");
        
        RuleFor(x => x.DepartureTime)
            .NotEmpty().WithMessage("is required")
            ;
        
        RuleFor(x => x.ArrivalTime)
            .NotEmpty().WithMessage("is required")
            .GreaterThanOrEqualTo(x => x.DepartureTime).WithMessage("is greater or equal DepartureTime");
        
        RuleFor(x => x.BusStopId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater 0");
    }
}