using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.BillManage.DTOs.Requests;

[ValidateNever]
public class BillItemRequest
{
    public int BillId { get; set; }
    public int TicketItemId { get; set; }
}