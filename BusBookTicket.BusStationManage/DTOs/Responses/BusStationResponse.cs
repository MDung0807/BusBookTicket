namespace BusBookTicket.BusStationManage.DTOs.Responses;

public class BusStationResponse
{
    public int BusStationId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Description { get; set; }
    public int Status;
    public int WardId { get; set; }
}