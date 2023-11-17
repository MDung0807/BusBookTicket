namespace BusBookTicket.Buses.DTOs.Requests;

public class SeatForm
{
    public int SeatId { get; set; }
    public string SeatNumber { get; set; }
    public int SeatTypeId { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int BusId { get; set; }
}