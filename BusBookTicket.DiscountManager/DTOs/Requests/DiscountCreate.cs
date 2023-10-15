namespace BusBookTicket.DiscountManager.DTOs.Requests;

public class DiscountCreate
{
    public string? name { get; set; }
    public string? description { get; set; }
    public int quantity { get; set; }
    public DateTime dateCreate { get; set; }
    public DateTime dateStart { get; set; }
    public DateTime dateEnd { get; set; }
    public string rankName { get; set; }
}