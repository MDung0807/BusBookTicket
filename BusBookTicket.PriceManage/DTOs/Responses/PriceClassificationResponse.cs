namespace BusBookTicket.PriceManage.DTOs.Responses;

public class PriceClassificationResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public int Status { get; set; }
    public int Id { get; set; }
}