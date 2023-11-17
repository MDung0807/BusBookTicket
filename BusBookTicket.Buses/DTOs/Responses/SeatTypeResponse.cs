namespace BusBookTicket.Buses.DTOs.Responses;

public class SeatTypeResponse
{
    public int TypeId { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
}