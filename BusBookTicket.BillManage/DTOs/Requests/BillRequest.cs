using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.BillManage.DTOs.Requests;


[ValidateNever]
public class BillRequest
{
    public DateTime DateDeparture { get; set; }
    public int RouteDetailStartId { get; set; }
    public int RouteDetailEndId { get; set; }
    public int DiscountId { get; set; } 
    public List<BillItemRequest> ItemsRequest { get; set; }
}