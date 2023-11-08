using BusBookTicket.AddressManagement.DTOs.Responses.District;

namespace BusBookTicket.AddressManagement.DTOs.Responses.Province;

public class ProvinceResponse
{
    public string FullName { get; set; }
    public List<DistrictResponse> Districts { get; set; }
}