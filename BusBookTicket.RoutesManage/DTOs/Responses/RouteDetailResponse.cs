namespace BusBookTicket.RoutesManage.DTOs.Responses;

public class RouteDetailResponse 
{
    public int RouteId { get; set; }
    public int Id { get; set; }
    public int IndexStation { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public int AddDay { get; set; }
    public double DiscountPrice { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public int BusStationId { get; set; }
    public string BusStationName { get; set; }
}

