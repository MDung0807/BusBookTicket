using BusBookTicket.AddressManagement.DTOs.Responses.Ward;

namespace BusBookTicket.AddressManagement.DTOs.Responses.District;

public class DistrictResponse
{
    public string FullName { get; set; }
    public List<WardResponse> Wards { get; set; }
}