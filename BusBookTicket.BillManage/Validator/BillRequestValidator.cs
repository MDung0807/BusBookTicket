using BusBookTicket.BillManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.BillManage.Validator;

public class BillRequestValidator : AbstractValidator<BillRequest>
{
    public BillRequestValidator()
    {
        RuleFor(x => x.BusStationStartId)
            .GreaterThan(0).WithMessage("must greater 0");
        
        RuleFor(x => x.BusStationEndId)
            .GreaterThan(0).WithMessage("must greater 0");
        
        // RuleFor(x => x.DiscountId)
        //     .GreaterThan(0).WithMessage("must greater 0");

        RuleForEach(x => x.ItemsRequest).SetValidator(new BillItemRequestValidator());
    }
}