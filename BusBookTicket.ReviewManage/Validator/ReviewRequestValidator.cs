using BusBookTicket.ReviewManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.ReviewManage.Validator;

public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
{
    public ReviewRequestValidator()
    {
        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0).WithMessage("Value is greater or equal 0")
            .LessThanOrEqualTo(5).WithMessage("Value is less or equal 5");

        RuleFor(x => x.BusId)
            .GreaterThan(0).WithMessage("Value is greater 0");
    }
}