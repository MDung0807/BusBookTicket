namespace BusBookTicket.Ticket.DTOs.Requests;

public class TicketFormUpdate
{
    public int ticketID { get; set; }
    public DateTime date { get; set; }
    public int busID { get; set; }
}