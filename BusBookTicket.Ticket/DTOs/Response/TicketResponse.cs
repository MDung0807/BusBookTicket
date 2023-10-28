namespace BusBookTicket.Ticket.DTOs.Response;

public class TicketResponse
{
    public int ticketID { get; set; }
    public DateTime date { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
    public string stationStart { get; set; }
    public string stationEnd { get; set; }
    public List<TicketItemResponse> ItemResponses { get; set; }
}