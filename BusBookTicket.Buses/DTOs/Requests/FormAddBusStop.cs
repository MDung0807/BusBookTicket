namespace BusBookTicket.Buses.DTOs.Requests;

public class FormAddBusStop
{
    public int Id { get; set; }
    public List<int> BusStopIds { get; set; }
}