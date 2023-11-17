namespace BusBookTicket.Buses.DTOs.Requests;

public class BusTypeForm
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int TotalSeats { get; set; }
    public int Status { get; set; }
}