using BusBookTicket.BusStationManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.BusStationManage.Validator;

public class BST_FormCreateValidator : AbstractValidator<BST_FormCreate>
{
    public BST_FormCreateValidator()
    {
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