namespace BusBookTicket.Buses.DTOs.Responses;

public class SeatResponse
{
    public int seatID { get; set; }
    public string seatNumber { get; set; }
    public int seatTypeID { get; set; }
    public int price { get; set; }
    public string description { get; set; }
}