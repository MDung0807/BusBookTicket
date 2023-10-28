namespace BusBookTicket.Ticket.DTOs.Response;

public class TicketItemResponse
{
    public int ticketID { get; set; }
    public int ticketItemID { get; set; }
    public int status { get; set; }
    public int price { get; set; }
    public string seatNumber { get; set; }
    public string seatType { get; set; }
}