namespace BusBookTicket.Buses.DTOs.Responses;

public class SeatTypeResponse
{
    public int typeID { get; set; }
    public string type { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public int status { get; set; }
}