namespace BusBookTicket.Buses.DTOs.Requests;

public class FormUpdateBus
{
    public int companyID { get; set; }
    public string busNumber { get; set; }
    public string busType { get; set; }
    public List<string> busStops { get; set; }
}