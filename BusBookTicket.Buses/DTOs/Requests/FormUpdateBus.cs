namespace BusBookTicket.Buses.DTOs.Requests;

public class FormUpdateBus
{
    public int busID { get; set; }
    public int companyID { get; set; }
    public string busNumber { get; set; }
    public int busType { get; set; }
}