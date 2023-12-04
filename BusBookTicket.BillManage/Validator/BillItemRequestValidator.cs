using BusBookTicket.BillManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.BillManage.Validator;

public class BillItemRequestValidator : AbstractValidator<BillItemRequest>
{
    public BillItemRequestValidator()
    {
        RuleFor(x => x.TicketItemId)
            .GreaterThan(0).WithMessage("must greater 0");
        
        RuleFor(x => x.BillId)
            .GreaterThan(0).WithMessage("must greater 0");
    }
}