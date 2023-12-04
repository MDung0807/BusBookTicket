using BusBookTicket.BusStationManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.BusStationManage.Validator;

public class BST_FormUpdateValidator : AbstractValidator<BST_FormUpdate>
{
    public BST_FormUpdateValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("is required");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("max character is 50");
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("max character is 50");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("is required")
            .MaximumLength(50).WithMessage("max character is 50");
        RuleFor(x => x.WardId)
            .GreaterThan(0).WithMessage("is required");
    }
}