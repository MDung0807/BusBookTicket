namespace BusBookTicket.RoutesManage.DTOs.Requests;

public class RouteDetailCreate
{
    public List<RouteDetailCreateItem> Items { get; set; }
}

public class RouteDetailCreateItem
{
    public int BusStationId { get; set; }
    public int RouteId { get; set; }
    public int IndexStation { get; set; }
    public TimeSpan? ArrivalTime { get; set; }
    public TimeSpan? DepartureTime { get; set; }
    public int AddDay { get; set; }
    public double DiscountPrice { get; set; }
    public int CompanyId { get; set; }
}