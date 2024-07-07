using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class SeatTypeFormCreateValidator : AbstractValidator<SeatTypeFormCreate>
{
    public SeatTypeFormCreateValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("is required");
        RuleFor(x => x.Price)
            .NotNull().WithMessage("is required")
            .LessThan(0).WithMessage("Price has greater than 0");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("is required");
        // RuleFor(x => x.CompanyId)
        //     .NotEmpty().WithMessage("is required");
    }
}