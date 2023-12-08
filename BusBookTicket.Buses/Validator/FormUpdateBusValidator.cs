using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class FormUpdateBusValidator : AbstractValidator<FormUpdateBus>
{
    public FormUpdateBusValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        // RuleFor(x => x.CompanyId)
        //     .NotEmpty().WithMessage("is required")
        //     .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.BusNumber)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("must be max 50 characters");
        RuleFor(x => x.BusTypeId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
    }
}