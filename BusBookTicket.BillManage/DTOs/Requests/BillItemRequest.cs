using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.BillManage.DTOs.Requests;

[ValidateNever]
public class BillItemRequest
{
    public int billID { get; set; }
    public List<int> seatNumber { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
}