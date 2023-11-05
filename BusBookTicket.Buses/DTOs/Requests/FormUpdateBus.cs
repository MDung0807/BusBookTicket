namespace BusBookTicket.Buses.DTOs.Requests;

public class FormUpdateBus
{
    public int Id { get; set; }
    public int companyID { get; set; }
    public string busNumber { get; set; }
    public int busTypeId { get; set; }
}