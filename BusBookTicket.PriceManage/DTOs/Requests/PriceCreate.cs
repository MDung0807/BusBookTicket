namespace BusBookTicket.PriceManage.DTOs.Requests;

public class PriceCreate
{
    public int CompanyId { get; set; }
    public int RouteId { get; set; }
    public double Surcharges { get; set; }
    public double Price { get; set; }
}