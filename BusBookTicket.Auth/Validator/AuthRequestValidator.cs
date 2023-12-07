using BusBookTicket.Auth.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Auth.Validator;

public class AuthRequestValidator : AbstractValidator<AuthRequest>
{
    public AuthRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("is required")
            .Length(4, 50).WithMessage("must be between 8 and 50 characters");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("is required")
            .Matches("[A-Z]").WithMessage("must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("must contain at least one numeric digit")
            .Matches("[!@#$%^&*(),.?\":{}|<>]")
            .WithMessage("must contain at least one special character (!@#$%^&*(),.?\":{}|<>)");
    }
}