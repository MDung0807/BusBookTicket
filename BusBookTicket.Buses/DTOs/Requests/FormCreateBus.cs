namespace BusBookTicket.Buses.DTOs.Requests;

public class FormCreateBus
{
    public int companyID { get; set; }
    public string busNumber { get; set; }
    public int busTypeID { get; set; }
    public int seatTypeID { get; set; }
    public List<int> listBusStopID { get; set; }
}