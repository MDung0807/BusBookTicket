namespace BusBookTicket.RoutesManage.DTOs.Responses;

public class RoutesResponse
{
    public int StationStartId { get; set; }
    public string StationStartName { get; set; }
    public string StationEndName { get; set; }
    public int StationEndId { get; set; }
}