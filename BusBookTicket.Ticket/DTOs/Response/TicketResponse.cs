namespace BusBookTicket.Ticket.DTOs.Response;

public class TicketResponse
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string BusNumber { get; set; }
    public string Introduction { get; set; }
    public string Company { get; set; }
    public List<StationResponse> ListStation { get; set; }
    public List<TicketItemResponse> ItemResponses { get; set; }
}

public class StationResponse
{
    public int StationId { get; set; }
    public string Station { get; set; }
}