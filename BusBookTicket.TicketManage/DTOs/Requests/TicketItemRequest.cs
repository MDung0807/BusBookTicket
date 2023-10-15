namespace BusBookTicket.TicketManage.DTOs.Requests;

public class TicketItemRequest
{
    public List<int> seatNumber { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
}