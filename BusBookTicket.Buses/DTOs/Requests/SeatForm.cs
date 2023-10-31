namespace BusBookTicket.Buses.DTOs.Requests;

public class SeatForm
{
    public int seatID { get; set; }
    public string seatNumber { get; set; }
    public int seatTypeID { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public int busID { get; set; }
}