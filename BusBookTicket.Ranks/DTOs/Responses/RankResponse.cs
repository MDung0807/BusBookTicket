namespace BusBookTicket.Ranks.DTOs.Responses;

public class RankResponse
{
    public int rankID { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int totalUser { get; set; }
}