using BusBookTicket.Ticket.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.Ticket.Validator;

public class TicketFormCreateValidator : AbstractValidator<TicketFormCreate>
{
    public TicketFormCreateValidator()
    {
        RuleForEach(x => x.TicketStations).SetValidator(new TicketStationDtoValidator());

        RuleFor(form => form.TicketStations)
            .Custom((stations, context) =>
            {
                var sortedStations = stations.OrderBy(ts => ts.IndexStation).ToList();
                TicketStationDto previousItem = null;

                foreach (var station in sortedStations)
                {
                    if (previousItem != null)
                    {
                        if (station.IndexStation <= previousItem.IndexStation)
                        {
                            context.AddFailure($"TicketStations: IndexStation should be greater than {previousItem.IndexStation}");
                        }

                        if (station.DepartureTime < previousItem.ArrivalTime)
                        {
                            context.AddFailure($"TicketStations: DepartureTime should be greater or equal to ArrivalTime of the previous station");
                        }
                    }

                    previousItem = station;
                }
            });

    }
}