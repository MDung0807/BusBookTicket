namespace BusBookTicket.ReviewManage.DTOs.Responses;

public class ReviewResponse
{
    public string Reviews { get; set; }
    public int BusId { get; set; }
    public int Rate { get; set; }
    public DateTime DateUpdate { get; set; }
    public int CustomerId { get; set; }
    public string FullName { get; set; }
}