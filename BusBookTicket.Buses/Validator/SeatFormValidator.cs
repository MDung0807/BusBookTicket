using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class SeatFormValidator : AbstractValidator<SeatForm>
{
    public SeatFormValidator()
    {
        RuleFor(x => x.SeatId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.SeatNumber)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("is be max 50 character");
        RuleFor(x => x.SeatTypeId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("is be max 50 character");
        RuleFor(x => x.BusId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
    }
}