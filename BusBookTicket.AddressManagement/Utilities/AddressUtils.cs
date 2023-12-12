using BusBookTicket.AddressManagement.DTOs.Responses;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.AddressManagement.Services.WardService;

namespace BusBookTicket.AddressManagement.Utilities;

public abstract class AddressUtils
{
    public static async Task<string> GetAddressDb(int wardId, IWardService wardService)
    {
        WardResponse response = await wardService.GetById(wardId);
        return response.FullName + ", " + response.District + ", "+ response.Province;
    }
    
    public static async Task<WardResponse> GetFullAddressDb(int wardId, IWardService wardService)
    {
        WardResponse response = await wardService.GetById(wardId);
        return response;
    }
}