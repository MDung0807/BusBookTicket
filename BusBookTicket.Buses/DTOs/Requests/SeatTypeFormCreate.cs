namespace BusBookTicket.Buses.DTOs.Requests;

public class SeatTypeFormCreate
{
    public string Type { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
    public bool IsCommon { get; set; }

}