namespace BusBookTicket.BusStationManage.DTOs.Responses;

public class BusStationResponse
{
    public int busStationID { get; set; }
    public string? name { get; set; }
    public string? address { get; set; }
    public string? description { get; set; }
    public int status;
}