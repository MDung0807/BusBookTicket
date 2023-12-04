namespace BusBookTicket.Ticket.DTOs.Requests;

public class SearchForm
{
    public DateTime DateTime { get; set; }
    public string StationStart { get; set; }
    public string StationEnd { get; set; }
}