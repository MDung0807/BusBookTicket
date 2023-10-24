namespace BusBookTicket.Buses.DTOs.Responses;

public class BusResponse
{
    public string busNumber { get; set; }
    public string busType { get; set; }
    public List<string> busStops { get; set; }
    public int status;
    public string company { get; set; }
}