namespace BusBookTicket.TicketManage.DTOs.Responses;

public class TicketResponse
{
    public string nameCustomer { get; set; }
    public DateTime dateDeparture { get; set; }
    public DateTime dateCreate { get; set; }
    public long totolPrice { get; set; }
    public string busStationStart { get; set; }
    public string busStationEnd { get; set; }
    public int discountID { get; set; } 
    public List<TicketItemResponse> items { get; set; }
}