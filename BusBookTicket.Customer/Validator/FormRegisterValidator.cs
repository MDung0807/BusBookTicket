using System.Data;
using BusBookTicket.CustomerManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.CustomerManage.Validator;

public class FormRegisterValidator : AbstractValidator<FormRegister>
{
    public FormRegisterValidator()
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

        RuleFor(x => x.Avatar);

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("is required")
            .Length(8, 50).WithMessage("must be between 8 and 50 characters");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("is required")
            .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("must contain at least one numeric digit")
            .Matches("[!@#$%^&*(),.?\":{}|<>]")
            .WithMessage("must contain at least one special character (!@#$%^&*(),.?\":{}|<>)");
    }
}