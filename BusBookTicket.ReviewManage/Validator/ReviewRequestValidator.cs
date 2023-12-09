using BusBookTicket.ReviewManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.ReviewManage.Validator;

public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
{
    public ReviewRequestValidator()
    {
        RuleFor(x => x.Rate)
            .GreaterThan(0).WithMessage("Value is greater 0");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("Value is greater 0");
    }
}