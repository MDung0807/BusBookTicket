using BusBookTicket.PriceManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.PriceManage.Validator;

public class PriceValidator : AbstractValidator<PriceCreate>
{
    public PriceValidator()
    {
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price has value greater 0");
        RuleFor(x => x.RouteId).NotEmpty().WithMessage("Price has been Route");
    }
}