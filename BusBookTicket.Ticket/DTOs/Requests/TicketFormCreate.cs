using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.Ticket.DTOs.Requests;

[ValidateNever]
public class TicketFormCreate
{
    public int BusId { get; set; }
    public int Price { get; set; }
    public List<TicketStationDto> TicketStations { get; set; }
}

public class TicketStationDto
{
    public int IndexStation { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public int DiscountPrice { get; set; }
    public int BusStopId { get; set; }
}