namespace BusBookTicket.Buses.DTOs.Requests;

public class FormCreateBus
{
    public int CompanyId { get; set; }
    public string BusNumber { get; set; }
    public int BusTypeId { get; set; }
    public int SeatTypeId { get; set; }
    public List<int> ListBusStopId { get; set; }
}