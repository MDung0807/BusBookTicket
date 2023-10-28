namespace BusBookTicket.Buses.DTOs.Requests;

public class SeatTypeFormUpdate
{
    public int typeID { get; set; }
    public string type { get; set; }
    public int seatTypeID { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public int companyID { get; set; }
}