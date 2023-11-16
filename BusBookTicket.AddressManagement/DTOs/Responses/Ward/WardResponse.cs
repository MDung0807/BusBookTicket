using BusBookTicket.AddressManagement.DTOs.Responses.District;

namespace BusBookTicket.AddressManagement.DTOs.Responses.Ward;

public class WardResponse
{
    public string FullName { get; set;}
    public string District { get; set; }
    public string Province { get; set; }
}