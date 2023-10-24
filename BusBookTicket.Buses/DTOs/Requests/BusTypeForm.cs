namespace BusBookTicket.Buses.DTOs.Requests;

public class BusTypeForm
{
    public string name { get; set; }
    public string description { get; set; }
    public int totalSeats { get; set; }
    public int status { get; set; }
}