namespace BusBookTicket.Ticket.DTOs.Requests;

public class TicketFormUpdate
{
    public int TicketId { get; set; }
    public DateTime Date { get; set; }
    public int BusId { get; set; }
}