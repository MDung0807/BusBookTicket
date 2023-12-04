using BusBookTicket.CustomerManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.CustomerManage.Validator;

public class FormUpdateValidator : AbstractValidator<FormUpdate>
{
    public FormUpdateValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("is required")
            .Length(10, 50).WithMessage("must be between 8 and 50 characters");

        RuleFor(x => x.DateOfBirth)
            .Empty();

        RuleFor(x => x.Address);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("is required")
            .Matches("^0[0-9]{9}$").WithMessage("Invalid format phoneNumber");

        RuleFor(x => x.Gender)
            .Empty();

        RuleFor(x => x.WardId);
    }
}