using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.DiscountManage.DTOs.Requests;

[ValidateNever]
public class DiscountCreate
{
    public string? name { get; set; }
    public string? description { get; set; }
    public int quantity { get; set; }
    public DateTime dateCreate { get; set; }
    public DateTime dateStart { get; set; }
    public DateTime dateEnd { get; set; }
    public string rankID { get; set; }
    public float value { get; set; }
}