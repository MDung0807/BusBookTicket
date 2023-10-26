namespace BusBookTicket.BillManage.DTOs.Responses;

public class BillItemResponse
{
    public List<int> seatNumber { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
}