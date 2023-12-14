using BusBookTicket.Auth.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Auth.Validator;

public class FormResetPassValidator : AbstractValidator<FormResetPass>
{
    public FormResetPassValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("is required")
            .Matches("^0[0-9]{9}$").WithMessage("Invalid format phoneNumber");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("is required")
            .Length(4, 50).WithMessage("must be between 8 and 50 characters");
        RuleFor(x => x.PasswordOld)
            .NotEmpty().WithMessage("is required")
            .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("must contain at least one numeric digit")
            .Matches("[!@#$%^&*(),.?\":{}|<>]")
            .WithMessage("must contain at least one special character (!@#$%^&*(),.?\":{}|<>)");
        
        RuleFor(x => x.PasswordNew)
            .NotEmpty().WithMessage("is required")
            .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("must contain at least one numeric digit")
            .Matches("[!@#$%^&*(),.?\":{}|<>]")
            .WithMessage("must contain at least one special character (!@#$%^&*(),.?\":{}|<>)");
        
        RuleFor(x => x.ConfirmPasswordNew)
            .Must((model, confirmPassword) => confirmPassword == model.PasswordNew)
            .WithMessage("must match the PasswordNew");
    }
}