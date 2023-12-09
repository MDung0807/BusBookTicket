namespace BusBookTicket.ReviewManage.DTOs.Requests;

public class ReviewRequest
{
    public string Reviews { get; set; }
    public int BusId { get; set; }
    public int Rate { get; set; }
}