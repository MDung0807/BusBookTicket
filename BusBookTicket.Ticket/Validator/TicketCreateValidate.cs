using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class TicketCreateValidate : AbstractValidator<TicketFormCreateManyBus>
{
    public TicketCreateValidate()
    {
        
    }
}