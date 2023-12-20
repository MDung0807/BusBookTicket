using BusBookTicket.RoutesManage.DTOs.Requests;
using FluentValidation;

namespace BusBookTicket.RoutesManage.Validator;

public class RouteDetailValidator : AbstractValidator<RouteDetailCreate>
{
    public RouteDetailValidator()
    {
        RuleFor(x => x.Items)
            .Custom((stations, context) =>
            {
                var sortedStations = stations.OrderBy(ts => ts.IndexStation).ToList();
                RouteDetailCreateItem previousItem = null;

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