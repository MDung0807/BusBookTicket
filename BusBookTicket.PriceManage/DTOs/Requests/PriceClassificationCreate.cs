namespace BusBookTicket.PriceManage.DTOs.Requests;

public class PriceClassificationCreate
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
}