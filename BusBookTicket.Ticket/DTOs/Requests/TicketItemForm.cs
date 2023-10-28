namespace BusBookTicket.Ticket.DTOs.Requests;

public class TicketItemForm
{
    public int ticketID { get; set; }
    public int ticketItemID { get; set; }
    public int status { get; set; }
    public int price { get; set; }
    public string seatNumber { get; set; }
}