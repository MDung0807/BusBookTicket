using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.BillManage.DTOs.Requests;


[ValidateNever]
public class BillRequest
{
    public DateTime DateDeparture { get; set; }
    public int BusStationStartId { get; set; }
    public int BusStationEndId { get; set; }
    public int DiscountId { get; set; } 
    public List<BillItemRequest> ItemsRequest { get; set; }
}