namespace BusBookTicket.BusStationManage.DTOs.Requests;

public class BST_FormCreate
{
    public string? name { get; set; }
    public string? address { get; set; }
    public string? description { get; set; }
    public int status { get; set; }
}