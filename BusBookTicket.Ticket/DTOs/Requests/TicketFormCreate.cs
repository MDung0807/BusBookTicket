using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.Ticket.DTOs.Requests;

[ValidateNever]
public class TicketFormCreate
{
    public DateTime date { get; set; }
    public int busID { get; set; }
}