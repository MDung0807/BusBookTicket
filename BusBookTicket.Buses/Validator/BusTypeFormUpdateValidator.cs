using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class BusTypeFormUpdateValidator : AbstractValidator<BusTypeFormUpdate>
{
    public BusTypeFormUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("must be max 50 characters");

        RuleFor(x => x.Description);

        RuleFor(x => x.TotalSeats)
            .GreaterThan(0).WithMessage("is greater than 0");

    }

}