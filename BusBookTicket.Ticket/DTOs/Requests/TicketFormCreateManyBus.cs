namespace BusBookTicket.Ticket.DTOs.Requests;

public class TicketFormCreateManyBus
{
    public List<int> BusIds { get; set; }
    public DateOnly DateOnly { get; set; }
    public int PriceClassificationId { get; set; }
    public List<TicketStationDto> TicketStations { get; set; }
}