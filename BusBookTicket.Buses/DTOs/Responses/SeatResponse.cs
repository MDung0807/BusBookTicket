namespace BusBookTicket.Buses.DTOs.Responses;

public class SeatResponse
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; }
    public int SeatTypeId { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
}