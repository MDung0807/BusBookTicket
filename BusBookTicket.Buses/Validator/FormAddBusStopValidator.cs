using BusBookTicket.Buses.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Buses.Validator;

public class FormAddBusStopValidator : AbstractValidator<FormAddBusStop>
{
    public FormAddBusStopValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
        
        RuleForEach(x => x.BusStopIds)
            .NotEmpty().WithMessage("is required")
            .GreaterThan(0).WithMessage("is greater than 0");
    }
}