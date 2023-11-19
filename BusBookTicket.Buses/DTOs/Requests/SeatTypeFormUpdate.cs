namespace BusBookTicket.Buses.DTOs.Requests;

public class SeatTypeFormUpdate
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int CompanyId { get; set; }
}