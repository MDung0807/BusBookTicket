namespace BusBookTicket.PriceManage.DTOs.Responses;

public class PriceResponse
{
    public int CompanyId { get; set; }
    public int RouteId { get; set; }
    public int StationStartId { get; set; }
    public int StationEndId { get; set; }
    public string StationStart { get; set; }
    public string StationEnd { get; set; }
    public double Surcharges { get; set; }
    public double Price { get; set; }
    public string CompanyName { get; set; }
    public int Status { get; set; }
    public int Id { get; set; }
}