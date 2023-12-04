namespace BusBookTicket.BusStationManage.DTOs.Requests;

public class BST_FormUpdate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int WardId { get; set; }
}