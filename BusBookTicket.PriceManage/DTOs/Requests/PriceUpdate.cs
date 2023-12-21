namespace BusBookTicket.PriceManage.DTOs.Requests;

public class PriceUpdate
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public int RouteId { get; set; }
    public double Surcharges { get; set; }
    public double Price { get; set; }
}