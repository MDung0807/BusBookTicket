namespace BusBookTicket.BillManage.DTOs.Requests;

public class BillItemRequest
{
    public int billID { get; set; }
    public List<int> seatNumber { get; set; }
    public string busNumber { get; set; }
    public string company { get; set; }
}