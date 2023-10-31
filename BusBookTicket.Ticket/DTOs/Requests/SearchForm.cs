namespace BusBookTicket.Ticket.DTOs.Requests;

public class SearchForm
{
    public DateTime dateTime { get; set; }
    public string stationStart { get; set; }
    public string stationEnd { get; set; }
}