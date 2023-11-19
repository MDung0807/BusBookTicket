namespace BusBookTicket.Ticket.DTOs.Response;

public class TicketItemResponse
{
    public int TicketId { get; set; }
    public int Id { get; set; }
    public int Status { get; set; }
    public int Price { get; set; }
    public string SeatNumber { get; set; }
    public string SeatType { get; set; }
}