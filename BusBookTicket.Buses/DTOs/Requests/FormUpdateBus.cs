namespace BusBookTicket.Buses.DTOs.Requests;

public class FormUpdateBus
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string BusNumber { get; set; }
    public int BusTypeId { get; set; }
}