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
            .NotEmpty().WithMessage("is required");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("is required");
        // RuleFor(x => x.CompanyId)
        //     .NotEmpty().WithMessage("is required");
    }
}