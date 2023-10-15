namespace BusBookTicket.TicketManage.DTOs.Responses;

public class TicketItemResponse
{
    public List<int> seatNumber { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
}