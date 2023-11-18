using BusBookTicket.AddressManagement.DTOs.Responses.Province;

namespace BusBookTicket.AddressManagement.DTOs.Responses.Region;

public class RegionResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ProvinceResponse> Provinces { get; set; }
}