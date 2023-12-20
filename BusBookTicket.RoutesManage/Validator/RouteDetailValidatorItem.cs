using BusBookTicket.RoutesManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.RoutesManage.Validator;

public class RouteDetailValidatorItem : AbstractValidator<RouteDetailCreateItem>
{
    public RouteDetailValidatorItem()
    {
        RuleFor(x => x.IndexStation)
            .GreaterThanOrEqualTo(0).WithMessage(" should be greater than or equal to 0");

        RuleFor(x => x.DepartureTime)
            .NotEmpty().WithMessage(" is required");

        RuleFor(x => x.ArrivalTime)
            .NotEmpty().WithMessage(" is required")
            .GreaterThanOrEqualTo(x => x.DepartureTime).WithMessage(" should be greater or equal to DepartureTime of the previous station");

        RuleFor(x => x.BusStationId)
            .NotEmpty().WithMessage(" is required")
            .GreaterThan(0).WithMessage(" should be greater than 0");

        RuleFor(x => x).Custom((dto, context) =>
        {
            if (dto.IndexStation >= 0)
            {
                if (((IValidationContext)context).ParentContext.RootContextData["PreviousItem"] is RouteDetailCreateItem previousItem)
                {
                    if (dto.IndexStation <= previousItem.IndexStation)
                    {
                        context.AddFailure($" should be greater than {previousItem.IndexStation}");
                    }

                    if (dto.DepartureTime < previousItem.ArrivalTime)
                    {
                        context.AddFailure(" should be greater or equal to ArrivalTime of the previous station");
                    }
                }
            }

            ((IValidationContext)context).ParentContext.RootContextData["PreviousItem"] = dto;
        });
    }
}