namespace BusBookTicket.BusStationManage.DTOs.Responses;

public class BusStationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int WardId { get; set; }
    public string AddressDb { get; set; }
}