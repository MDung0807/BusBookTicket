namespace BusBookTicket.BillManage.DTOs.Responses;

public class BillResponse
{
    public string nameCustomer { get; set; }
    public DateTime dateDeparture { get; set; }
    public DateTime dateCreate { get; set; }
    public long totolPrice { get; set; }
    public string busStationStart { get; set; }
    public string busStationEnd { get; set; }
    public string discount { get; set; } 
    public List<BillItemResponse> items { get; set; }
}