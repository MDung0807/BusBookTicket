using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class TicketFormCreateValidator : AbstractValidator<TicketFormCreate>
{
    public TicketFormCreateValidator()
    {
        RuleForEach(x => x.TicketStations).SetValidator(new TicketStationDtoValidator());

    }
}