namespace BusBookTicket.Buses.DTOs.Responses;

public class BusResponse
{
    public string BusNumber { get; set; }
    public string BusType { get; set; }
    public List<string> BusStops { get; set; }
    public int Status;
    public string Company { get; set; }
}