namespace BusBookTicket.Buses.DTOs.Responses;

public class BusTypeResponse
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int TotalSeats { get; set; }
    public int Status { get; set; }
    public int TotalBus { get; set; }
}