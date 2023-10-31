namespace BusBookTicket.Buses.DTOs.Responses;

public class BusTypeResponse
{
    public int busTypeID {  get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int totalSeats { get; set; }
    public int status { get; set; }
}