using BusBookTicket.RoutesManage.DTOs.Responses;

namespace BusBookTicket.Buses.DTOs.Responses;

public class BusResponse
{
    public int Id { get; set; }
    public string BusNumber { get; set; }
    public string BusType { get; set; }
    public int TotalSeat { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public string Company { get; set; }
    
    public List<RoutesResponse> Routes { get; set; }
}