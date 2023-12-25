namespace BusBookTicket.Ticket.DTOs.Requests;

public class SearchForm
{
    public DateTime DateTime { get; set; }
    public string StationStart { get; set; }
    public string StationEnd { get; set; }
    public List<int> CompanyIds { get; set; }
    public bool PriceIsDesc { get; set; }
    public List<int> TimeInDays { get; set; }
}