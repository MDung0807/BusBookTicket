using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.AddressManagement.Services.WardService;

namespace BusBookTicket.AddressManagement.Utilities;

public class AddressResponse
{
    public static async Task<string> GetAddressDb(int wardId, IWardService wardService)
    {
        WardResponse response = await wardService.GetById(wardId);
        return response.FullName + ", " + response.District + ", "+ response.Province;
    }
}