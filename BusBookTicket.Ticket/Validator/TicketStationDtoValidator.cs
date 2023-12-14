using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class TicketStationDtoValidator : AbstractValidator<TicketStationDto>
{
    public TicketStationDtoValidator()
    {
        RuleFor(x => x.IndexStation)
            .GreaterThanOrEqualTo(0).WithMessage("IndexStation should be greater than or equal to 0");

        RuleFor(x => x.DepartureTime)
            .NotEmpty().WithMessage("DepartureTime is required");

        RuleFor(x => x.ArrivalTime)
            .NotEmpty().WithMessage("ArrivalTime is required")
            .GreaterThanOrEqualTo(x => x.DepartureTime).WithMessage("ArrivalTime should be greater or equal to DepartureTime of the previous station");

        RuleFor(x => x.BusStopId)
            .NotEmpty().WithMessage("BusStopId is required")
            .GreaterThan(0).WithMessage("BusStopId should be greater than 0");

        RuleFor(x => x).Custom((dto, context) =>
        {
            if (dto.IndexStation > 0)
            {
                var previousItem = ((IValidationContext)context).ParentContext.RootContextData["PreviousItem"] as TicketStationDto;

                if (previousItem != null)
                {
                    if (dto.IndexStation <= previousItem.IndexStation)
                    {
                        context.AddFailure($"IndexStation should be greater than {previousItem.IndexStation}");
                    }

                    if (dto.DepartureTime < previousItem.ArrivalTime)
                    {
                        context.AddFailure("DepartureTime should be greater or equal to ArrivalTime of the previous station");
                    }
                }
            }

            ((IValidationContext)context).ParentContext.RootContextData["PreviousItem"] = dto;
        });
    }
}