using BusBookTicket.RoutesManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.RoutesManage.Validator;

public class RouteValidator : AbstractValidator<RoutesCreate>
{
    public RouteValidator()
    {
        RuleFor(x => x.StationEndId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("Value is greater 0");
        
        RuleFor(x => x.StationStartId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("Value is greater 0");

        RuleFor(x => x.StationEndId)
            .NotEqual(p => p.StationStartId).WithMessage("Station Start is not equal station end");
    }
}