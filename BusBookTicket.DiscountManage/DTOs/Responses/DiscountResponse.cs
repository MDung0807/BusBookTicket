namespace BusBookTicket.DiscountManage.DTOs.Responses;

public class DiscountResponse
{
    public int discountID { get; set; }
    public string? name { get; set; }
    public string? description { get; set; }
    public int quantity { get; set; }
    public DateTime dateCreate { get; set; }
    public DateTime dateStart { get; set; }
    public DateTime dateEnd { get; set; }
    public string rankID { get; set; }
}