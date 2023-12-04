using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class SearchFormValidator : AbstractValidator<SearchForm>
{
    public SearchFormValidator()
    {
        RuleFor(x => x.DateTime)
            .NotEmpty().WithMessage("is required");
        RuleFor(x => x.StationStart)
            .NotEmpty().WithMessage("is required");
        RuleFor(x => x.StationEnd)
            .NotEmpty().WithMessage("is required");
    }
}