namespace BusBookTicket.AddressManagement.DTOs.Responses.Ward;

public class WardResponse
{
    public int Id { get; set; }
    public string FullName { get; set;}
    public string District { get; set; }
    public int DistrictId { get; set; }
    public string Province { get; set; }
    public int ProvinceId { get; set; }
}