namespace BusBookTicket.BillManage.DTOs.Requests;

public class BillRequest
{
    public int customerID { get; set; }
    public DateTime dateDeparture { get; set; }
    public long totolPrice { get; set; }
    public string busStationStart { get; set; }
    public string busStationEnd { get; set; }
    public int discountID { get; set; } 
    public List<BillItemRequest> itemsRequest { get; set; }
}