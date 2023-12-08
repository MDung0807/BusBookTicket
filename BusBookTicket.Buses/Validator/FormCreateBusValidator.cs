using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class FormCreateBusValidator : AbstractValidator<FormCreateBus>
{
    public FormCreateBusValidator()
    {
        // RuleFor(x => x.CompanyId)
        //     .NotEmpty().WithMessage("is required")
        //     .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.BusNumber)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("must be max 50 characters");
        RuleFor(x => x.BusTypeId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        RuleFor(x => x.SeatTypeId)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        
        RuleForEach(x => x.ListBusStopId)
            .GreaterThan(0).WithMessage("is greater than 0");

        RuleFor(x => x.BusNumber);
    }
}