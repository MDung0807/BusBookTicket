using BusBookTicket.PriceManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.PriceManage.Validator;

public class PriceClassificationCreateValidator : AbstractValidator<PriceClassificationCreate>
{
    public PriceClassificationCreateValidator()
    {
        RuleFor(x => x.Value).GreaterThanOrEqualTo(0).WithMessage("Value is greater or equal 0");
    }
}