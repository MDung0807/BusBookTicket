using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class SeatTypeFormUpdateValidator : AbstractValidator<SeatTypeFormUpdate>
{
    public SeatTypeFormUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("greater 0");
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("is required");
        // RuleFor(x => x.Price)
        //     // .NotEmpty().WithMessage("is required")
        //     // .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("is required");
    }
}